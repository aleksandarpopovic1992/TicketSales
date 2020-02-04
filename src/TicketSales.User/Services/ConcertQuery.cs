using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TicketSales.User.Models;
using TicketSales.User.Projection.Concert;

namespace TicketSales.User.Services
{
    public class ConcertQuery : IConcertQuery
    {
        public IConcertDocument _concerts;

        public ConcertQuery(IConcertDocument concerts)
        {
            _concerts = concerts;
        }

        public IEnumerable<ConcertModel> GetConcerts()
        {
           var concerts =  _concerts.GetConcerts();
            IList<ConcertModel> concertModels = new List<ConcertModel>();
            foreach(var concert in concerts)
            {
                concertModels.Add(new ConcertModel(concert.Id,concert.Title));
            }

            return concertModels;
        }

    }
}
