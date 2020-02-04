 using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using TicketSales.Messages.Commands;
using TicketSales.User.Models;
using TicketSales.User.Services;

namespace TicketSales.User.Controllers
{
    public class TicketController : Controller
    {
        private readonly IBus _bus;
        private readonly int _userId = 5; // kada se uvede autorizacija i autentifikacija ovaj id se nece zakucavati
        private readonly IConcertQuery _concertQuery;
        private readonly ITicketQuery _ticketQuery;

        public TicketController(IBus bus,  IConcertQuery concertQuery, ITicketQuery ticketQuery)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _concertQuery = concertQuery;
            _ticketQuery = ticketQuery;                              
        }

        public IActionResult Index()
        {
            return View(_ticketQuery.GetTicketsBy(_userId));
        }

        public IActionResult Buy()
        {
            IEnumerable<ConcertModel> concerts = _concertQuery.GetConcerts();
            IList<SelectListItem> selectListItems = new List<SelectListItem>();
            foreach (var concert in concerts)
            {
                selectListItems.Add(new SelectListItem { Value = concert.Id.ToString(), Text = concert.Title });
            }
            return View(new BuyTicketsViewModel() { ConcertIds = selectListItems });
        }


        [HttpPost]
        public IActionResult Buy(BuyTicketsViewModel buyTicketsViewModel)
        {
            if (!ModelState.IsValid)
            {
                return View(buyTicketsViewModel);
            }

            BuyConcertTicketsCommand buyTicketsCommand = new BuyConcertTicketsCommand(buyTicketsViewModel.TicketNumber, _userId, buyTicketsViewModel.ConcertId);

            _bus.Publish(buyTicketsCommand);

            return RedirectToAction("Index");

        }
    }
}