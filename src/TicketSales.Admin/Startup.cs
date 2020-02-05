using System;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketSales.Admin.Consumers;
using TicketSales.Admin.Models;
using TicketSales.Admin.Projection.Concert;
using TicketSales.Admin.Projection.Ticket;
using TicketSales.Admin.Services;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;
using TicketSales.Shared;
using TicketSales.Shared.Factory;

namespace TicketSales.Admin
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.Configure<CookiePolicyOptions>(options =>
            {
                // This lambda determines whether user consent for non-essential cookies is needed for a given request.
                options.CheckConsentNeeded = context => true;
                options.MinimumSameSitePolicy = SameSiteMode.None;
            });

            services.AddSingleton<TestMessageStore>();
            services.AddSingleton<IConcertQuery, ConcertQuery>();
            services.AddSingleton<IConcertDocument, ConcertDocument>();
            services.AddSingleton<IProjectorFactory<ConcertTicketsBoughtEvent>, TicketsBoughtEventProjectorFactory>();
            services.AddSingleton<IProjectorFactory<ConcertCreatedEvent>, ConcertCreatedEventProjectorFactory>();
            services.AddSingleton<IEventStore, EventStore>();
            services.AddScoped<ConcertCreatedEventHandler>();
            services.AddScoped<ConcertTicketsBoughtEventHandler>();

            services.AddMassTransit(x =>
            {

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host("localhost", "aleksandar", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.ReceiveEndpoint(host, "admin", e =>
                    {
                        e.PrefetchCount = 16;

                        e.Consumer<TestEventHandler>(provider);
                        e.Consumer<ConcertCreatedEventHandler>(provider);
                        e.Consumer<ConcertTicketsBoughtEventHandler>(provider);

                        EndpointConvention.Map<TestCommand>(new Uri("rabbitmq://localhost/aleksandar/core"));
                        EndpointConvention.Map<CreateConcertCommand>(new Uri("rabbitmq://localhost/aleksandar/core"));
                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });

            services.AddHostedService<BusService>();

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseCookiePolicy();

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "{controller=Concert}/{action=Index}/");
            });
        }
    }
}
