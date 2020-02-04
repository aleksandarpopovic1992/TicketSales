using NEventStore;
using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;
using TicketSales.Core.Domain.Factories;
using TicketSales.Core.Domain.Repositories;

namespace TicketSales.Core.Infrastructe
{
    public class ConcertRepository : IConcertRepository
    {
        private readonly IStoreEvents _eventStore;

        private readonly IConcertFactory _concertFactory;

        private static readonly object _object = new object();

        public ConcertRepository(IStoreEvents eventStore, IConcertFactory concertFactory)
        {
            _eventStore = eventStore;
            _concertFactory = concertFactory;
        }

        public bool TryFindBy(Guid concertId, out Concert concert)
        {
            var bucketId = GetBucketId(concertId);

            var fromEventNumber = 0;
            var toEventNumber = int.MaxValue;

            concert = _concertFactory.Create();
            
            using (var stream = _eventStore.OpenStream(bucketId, concertId.ToString(), fromEventNumber, toEventNumber))
            {
              
                foreach (var @event in stream.CommittedEvents)
                {
                    concert.Apply((dynamic)@event.Body);
                }

            }

            return concert.Id != Guid.Empty; 

        }

        public void Add(Concert concert)
        {
            lock (_object)
            {
                var bucketId = GetBucketId(concert.Id);

                using (var eventStream = _eventStore.CreateStream(bucketId, concert.Id.ToString()))
                {
                    foreach (var @event in concert.Changes)
                    {
                        eventStream.Add(new EventMessage() { Body = @event });
                    }

                    eventStream.CommitChanges(Guid.NewGuid());
                }
            }
        }

        public void Save(Concert concert)
        {
            lock (_object)
            {
                var bucketId = GetBucketId(concert.Id);

                using (var eventStream = _eventStore.OpenStream(bucketId, concert.Id.ToString(), 0, int.MaxValue))
                {
                    foreach (var @event in concert.Changes)
                    {
                        eventStream.Add(new EventMessage() { Body = @event });
                    }

                    eventStream.CommitChanges(Guid.NewGuid());

                }
            }
        }

        private string GetBucketId(Guid concertId)
        {
            return string.Format("{0}-{1}", typeof(Concert).Name, concertId);
        }

        public Guid GetNextId()
        {
            return Guid.NewGuid();
        }
    }
}
