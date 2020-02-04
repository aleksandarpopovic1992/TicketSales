using MassTransit;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using TicketSales.Core.Application.EventHandler;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Exception;
using TicketSales.Core.Domain.Factories;
using TicketSales.Core.Domain.Repositories;
using TicketSales.Messages.Commands;
using TicketSales.Messages.Events;

namespace TicketSales.Core.Application
{

    // logiku za rukovanje komandama treba izmestiti u drugu klasu, pokupiti poruku iz contexta i proslediti dalje
    public class CreateConcertCommandHandler : IConsumer<CreateConcertCommand>
    {
        public IConcertRepository ConcertRepository { get; set; }
        public IEventPublisherFactory EventHandlerFactory { get; set; }
        public IConcertFactory ConcertFactory { get; set; }
        public ILogger Logger { get; set;}

        public CreateConcertCommandHandler(IConcertRepository concertRepository, IEventPublisherFactory eventHandlerFactory, IConcertFactory concertFactory, ILogger logger)
        {
            ConcertRepository = concertRepository;
            EventHandlerFactory = eventHandlerFactory;
            ConcertFactory = concertFactory;
            Logger = logger;
        }

        public Task Consume(ConsumeContext<CreateConcertCommand> context)
        {
            CreateConcertCommand command = context.Message;

            Logger.LogInformation($"Processing command Create concert: Maximum number of tickets:{command.MaximumNumberOfTickets}, Place: {command.Place}, Title: {command.Title} - started. ");

            try
            {

                Concert concert = ConcertFactory.Create(command.Place, command.MaximumNumberOfTickets, command.Date, command.Title, ConcertRepository.GetNextId());

                ConcertRepository.Add(concert);

                foreach (var @event in concert.Changes)
                {
                    var publisher = EventHandlerFactory.CreatePublisher(@event);
                    publisher.Publish(@event, context);
                }

                Logger.LogInformation($"Processing command Create concert: Maximum number of tickets:{command.MaximumNumberOfTickets}, Place: {command.Place}, Title: {command.Title} - finished successfully. ");
            }
            catch (IntialTicketsCapacityNotSet ticketsCapacityNotSetException)
            {
                Logger.LogError($"{ticketsCapacityNotSetException.Message}. Can not execute command Create concert: Maximum number of tickets:{command.MaximumNumberOfTickets}, Place: {command.Place}, Title: {command.Title}.Command rejected. ");
            }
            return Task.CompletedTask; 
        }
    }
}
