using System;
using System.Collections.Generic;
using System.Text;

namespace TicketSales.Core.Application
{
    public interface ILogger
    {
        void LogError(string message);

        void LogInformation(string message);

    }
}
