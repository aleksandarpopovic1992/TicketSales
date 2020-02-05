using MassTransit;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using NEventStore;
using System;
using System.Collections.Generic;
using TicketSales.Core.Application;
using TicketSales.Core.Application.EventHandler;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Factories;
using TicketSales.Core.Domain.Repositories;
using TicketSales.Core.Infrastructe;
using TicketSales.Core.Infrastructure;

namespace TicketSales.Core.Web
{
    public class Startup
    {
        // This method gets called by the runtime. Use this method to add services to the container.
        // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
        public void ConfigureServices(IServiceCollection services)
        {

            services.AddSingleton(typeof(IStoreEvents), Wireup.Init().UsingInMemoryPersistence().Build());


            services.AddSingleton(typeof(IEventPublisherFactory), typeof(EventHandlerFactory));

            services.AddSingleton<IConcertRepository, ConcertRepository>();
            Dictionary<Type, IEventPublisher> eventHandlers = new Dictionary<Type, IEventPublisher>() { { typeof(ConcertCreatedDomainEvent), new ConcertCreatedEventHandler() }, { typeof(ConcertTicketsBoughtDomainEvent), new ConcertTicketsBoughtEventHandler() } };
            services.AddSingleton(typeof(Dictionary<Type, IEventPublisher>), eventHandlers);
            services.AddSingleton<IConcertFactory, ConcertFactory>();
            services.AddSingleton<IConcertFactory, ConcertFactory>();
            services.AddSingleton<ILogger, ConsoleLogger>();

            services.AddScoped<CreateConcertCommandHandler>();
            services.AddScoped<BuyConcertTicketsCommandHandler>();

            services.AddMassTransit(x =>
            {

                x.AddBus(provider => Bus.Factory.CreateUsingRabbitMq(cfg =>
                {
                    var host = cfg.Host("localhost", "aleksandar", h => { h.Username("guest"); h.Password("guest"); });

                    cfg.ReceiveEndpoint(host, "core", e =>
                    {
                        e.PrefetchCount = 16;

                        e.Consumer<TestCommandHandler>(provider);
                        e.Consumer<CreateConcertCommandHandler>(provider);
                        e.Consumer<BuyConcertTicketsCommandHandler>(provider);

                    });

                    // or, configure the endpoints by convention
                    cfg.ConfigureEndpoints(provider);
                }));
            });



            services.AddHostedService<BusService>();



        }
        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.Run(async (context) =>
            {
                await context.Response.WriteAsync("Hello World!");

            });






        }

    }
}

