using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;
namespace Cinemo.Pages
{
  public class BookTicketModel : PageModel
  {
    private readonly TicketService ticketService;
    private readonly ShowTimeService showTimeService;

    [BindProperty(SupportsGet = true)]
    public int showtime { get; set; }

    public ShowTime Showtime { get; set; }

    public List<Ticket> BookedTickets { get; set; }

    public BookTicketModel(TicketService ticketService)
    {
      this.ticketService = ticketService;
    }

    public void InitDate()
    {
      BookedTickets = ticketService.GetBookedTickets(showtime);
    }

    public void OnGet()
    {
    }
  }
}
