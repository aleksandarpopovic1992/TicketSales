using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain.Events;
using TicketSales.Core.Domain.Exception;

namespace TicketSales.Core.Domain
{
    public class Concert : EventSourcedAggregate
    {
        public Tickets MaximumNumberOfTickets { get; private set; }

        public Tickets BoughtTickets { get; private set; }

        public string Place { get; private set; }

        public string Title { get; private set; }

        public DateTime Date { get; private set; }

        public Concert() { }

        public Concert(string place, int maximumNumberOfTickets,DateTime date, string title, Guid concertId)
        {
            if (maximumNumberOfTickets < 1)
            {
                throw new IntialTicketsCapacityNotSet("Quanitity of tickets for sale not set.");
            }

            Causes(new ConcertCreatedDomainEvent(maximumNumberOfTickets, place,date,title,concertId));
        }

        public void BuyTickets(int numberOfTickets, long userId)
        {
            Tickets ticketsForBuying = new Tickets(numberOfTickets);
            Tickets newNumberOfBoughtTickets = ticketsForBuying.AddNumberOf(BoughtTickets);

            if (newNumberOfBoughtTickets.HasMoreThan(MaximumNumberOfTickets))
            {
                throw new MaximumNumberOfTicketsReached("Maximum number of tickets for sale reached.") ;
            }

            Causes(new ConcertTicketsBoughtDomainEvent(numberOfTickets,userId,Id));
        }

        public override void Apply(DomainEvent changes)
        {
            When((dynamic)changes);
            Version = Version++;
        }

        private void Causes(DomainEvent @event)
        {
            Changes.Add(@event);
            Apply(@event);
        }

        private void When(ConcertCreatedDomainEvent concertCreatedEvent)
        {
            MaximumNumberOfTickets = new Tickets(concertCreatedEvent.NumberOfTickets);
            BoughtTickets = new Tickets(0);
            Place = concertCreatedEvent.Place;
            Title = concertCreatedEvent.Title;
            Date = concertCreatedEvent.Date;
            Id = concertCreatedEvent.ConcertId;
        }

        private void When(ConcertTicketsBoughtDomainEvent concertTicketsBoughtEvent)
        {
            BoughtTickets = BoughtTickets.AddNumberOf(new Tickets(concertTicketsBoughtEvent.NumberOfTicketsToBuy));

        }
    }
}
