using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;
using TicketSales.Shared;
using TicketSales.Shared.Factory;
using TicketSales.User.Consumers;
using TicketSales.User.Models;
using TicketSales.User.Projection.Concert;

using TicketSales.User.Projection.Ticket;
using TicketSales.User.Services;

namespace TicketSales.User
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
            services.AddSingleton<ITicketQuery, TicketQuery>();
            services.AddSingleton<ITicketDocument, TicketDocument>();
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
                    var host = cfg.Host("localhost", "tickets", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.ReceiveEndpoint(host, "user", e =>
                    {
                        e.PrefetchCount = 16;

                        e.Consumer<TestEventHandler>(provider);

                        e.Consumer<ConcertTicketsBoughtEventHandler>(provider);

                        e.Consumer<ConcertCreatedEventHandler>(provider);


                        EndpointConvention.Map<BuyConcertTicketsCommand>(new Uri("rabbitmq://localhost/tickets/core"));

                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });

            services.AddHostedService<BusService>();
            services.AddMemoryCache();
            services.AddSession();
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, Microsoft.AspNetCore.Hosting.IHostingEnvironment env)
        {
            app.UseSession();
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
                    template: "{controller=Ticket}/{action=Index}/");
            });
        }
    }
}
