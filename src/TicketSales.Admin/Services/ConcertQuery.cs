using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.Admin.Models;
using TicketSales.Admin.Projection.Concert;

namespace TicketSales.Admin.Services
{
    public class ConcertQuery : IConcertQuery
    {
        private IConcertDocument _concerts; 

        public ConcertQuery(IConcertDocument concerts)
        {
            _concerts = concerts;
        }

        public IEnumerable<ConcertViewModel> GetConcerts()
        {
            var allConcerts = _concerts.GetConcerts();

            var result = new List<ConcertViewModel>(allConcerts.Count());

            foreach (var concert in allConcerts)
            {
                result.Add(CreateConcertVM(concert));
            }

            return result;

        }


        private ConcertViewModel CreateConcertVM(ConcertProjection concertProjection)
        {
            return new ConcertViewModel() { Id = concertProjection.Id, Title = concertProjection.Title, TicketsSoldOut = concertProjection.TicketBought, MaximumNumberOfTickets = concertProjection.MaximumNumberOfTickets };
        } 
    }
}
