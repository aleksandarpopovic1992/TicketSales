using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TicketSales.Admin.Projection.Concert
{
	public class ConcertDocument : IConcertDocument
	{
		private IDictionary<Guid, ConcertProjection> _concerts;
        private static readonly object _object = new object();

        public ConcertDocument()
		{
			_concerts = new Dictionary<Guid, ConcertProjection>();
		}

		public void Add(ConcertProjection item)
		{
            lock (_object)
            {
                _concerts[item.Id] = item;
            }			
		}

		public IEnumerable<ConcertProjection> GetConcerts()
		{
			return new List<ConcertProjection>(_concerts.Values);
		}


		public bool TryGetConcert(Guid id, out ConcertProjection concertProjection)
		{
			return _concerts.TryGetValue(id,out concertProjection);
		}

        public void Update(ConcertProjection concert)
        {
            lock (_object)
            {
                if (!TryGetConcert(concert.Id, out ConcertProjection concertForUpdate))
                {
                    return;
                }

                _concerts[concert.Id] = concert;
            }
            
        }
    }
}
