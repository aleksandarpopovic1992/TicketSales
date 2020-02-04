using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.User.Models;
using TicketSales.User.Projection.Ticket;

namespace TicketSales.User.Services
{
    public class TicketQuery : ITicketQuery
    {
        private ITicketDocument _tickets;

        public TicketQuery(ITicketDocument tickets)
        {
            _tickets = tickets;
        }


        public IEnumerable<TicketViewModel> GetTicketsBy(long userId)
        {
            IEnumerable<TicketsBoughtProjection> ticketsByUser = _tickets.GetBy(userId);

            IList<TicketViewModel> result = new List<TicketViewModel>(ticketsByUser.Count());

            foreach (var ticketsProjection in ticketsByUser)
            {
                result.Add(CreateTicketView(ticketsProjection));
            }

            return result;
        }

        private TicketViewModel CreateTicketView(TicketsBoughtProjection ticketsProjection)
        {
            return new TicketViewModel()
            {
                ConcertId = ticketsProjection.ConcertId,
                ConcertTitle = ticketsProjection.Title,
                NumberOfTickets = ticketsProjection.TicketsBought
            };

        }

    }
}
