using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Domain;

namespace TicketSales.Core.Domain.Repositories
{
   public  interface IConcertRepository
    {
        bool TryFindBy(Guid concertId, out Concert concert);

        void Add(Concert concert);

        void Save(Concert concert);

        Guid GetNextId();

    }
}
