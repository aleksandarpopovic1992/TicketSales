using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MassTransit;
using Microsoft.AspNetCore.Mvc;
using TicketSales.Admin.Models;
using TicketSales.Admin.Services;
using TicketSales.Messages.Commands;

namespace TicketSales.Admin.Controllers
{
    public class ConcertController : Controller
    {
        private readonly IBus _bus;
        private readonly IConcertQuery _query;

        public ConcertController(IBus bus, IConcertQuery query)
        {
            _bus = bus ?? throw new ArgumentNullException(nameof(bus));
            _query = query;
        }

        public IActionResult Index()
        {
            return View(_query.GetConcerts());
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Create(CreateConcertViewModel createConcertViewModel)
        {
            if (string.IsNullOrEmpty(createConcertViewModel.Title))
            {
                return View(createConcertViewModel);
            }

            CreateConcertCommand createConcertCommand = new CreateConcertCommand(createConcertViewModel.NumberOfTickets,createConcertViewModel.Place
                ,DateTime.Now,createConcertViewModel.Title );

            _bus.Publish(createConcertCommand);

            return RedirectToAction("Index");
        }





    }
}