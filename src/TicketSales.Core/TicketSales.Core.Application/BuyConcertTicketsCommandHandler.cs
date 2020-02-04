using MassTransit;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Core.Application.EventHandler;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Exception;
using TicketSales.Core.Domain.Repositories;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application
{
    public class BuyConcertTicketsCommandHandler : IConsumer<BuyConcertTicketsCommand>
    {
        public IConcertRepository ConcertRepository { get; set; }
        public IEventPublisherFactory EventHandlerFactory { get; set; }
        public ILogger Logger { get; set; }

        public BuyConcertTicketsCommandHandler(IConcertRepository concertRepository, IEventPublisherFactory eventHandlerFactory, ILogger logger)
        {
            ConcertRepository = concertRepository;
            EventHandlerFactory = eventHandlerFactory;
            Logger = logger;
        }

        public Task Consume(ConsumeContext<BuyConcertTicketsCommand> context)
        {
           
            BuyConcertTicketsCommand command = context.Message;
            Logger.LogInformation($"Processing command Buy concert tickets: Tickets to buy:{command.NumberOfTicketsToBuy}, User id: {command.UserId}, Concert id: {command.ConcertId} - started. ");

            try
            {
                /// kod unutar try bloka bi trebao da ide u poseban hendler
                Concert concert;

                if (!ConcertRepository.TryFindBy(command.ConcertId, out concert))
                {
                    return Task.CompletedTask;
                }

                concert.BuyTickets(command.NumberOfTicketsToBuy, command.UserId);

                ConcertRepository.Save(concert);

                foreach (var @event in concert.Changes)
                {
                    var handler = EventHandlerFactory.CreatePublisher(@event);
                    handler.Publish(@event, context);
                }

                Logger.LogInformation($"Processing command Buy concert tickets: Tickets to buy:{command.NumberOfTicketsToBuy}, User id: {command.UserId}, Concert id: {command.ConcertId} - finished succesfully. ");

            } catch(MaximumNumberOfTicketsReached ticketsReachedException)
            {
                Logger.LogError($"{ticketsReachedException.Message}. Can not execute command Buy concert tickets: Tickets to buy:{command.NumberOfTicketsToBuy}, User id: {command.UserId}, Concert id: {command.ConcertId}.Command rejected. ");
            }
            return Task.CompletedTask;


        }
    }
}
