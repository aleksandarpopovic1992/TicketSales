using System;
using System.Collections.Generic;
using System.Text;
using TicketSales.Core.Application;

namespace TicketSales.Core.Infrastructure
{
    public class ConsoleLogger : ILogger
    {


        public void LogError(string message)
        {
            string formattedMessage = $"[{DateTime.Now}][ERROR]{message}";
            Console.WriteLine(formattedMessage);
        }

        public void LogInformation(string message)
        {
            string formattedMessage = $"[{DateTime.Now}][INFO]{message}";
            Console.WriteLine(formattedMessage);
        }


    }
}
