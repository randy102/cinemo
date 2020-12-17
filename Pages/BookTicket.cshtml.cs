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
    private readonly TicketTypeService ticketTypeService;
    private readonly UserService userService;

    [BindProperty(SupportsGet = true)]
    public int showtimeId { get; set; }

    [BindProperty]
    public TicketBookDto CreateDto { get; set; }

    public ShowTime Showtime { get; set; }
    public List<Ticket> BookedTickets { get; set; }
    public List<TicketType> TicketTypes { get; set; }
    public string ErrorMessage { get; set; }

    public BookTicketModel(
      TicketService ticketService,
      ShowTimeService showTimeService,
      TicketTypeService ticketTypeService,
      UserService userService
    )
    {
      this.ticketService = ticketService;
      this.showTimeService = showTimeService;
      this.ticketTypeService = ticketTypeService;
      this.userService = userService;
    }

    public void InitData()
    {
      BookedTickets = ticketService.GetBookedTickets(showtimeId);
      Showtime = showTimeService.GetDetail(showtimeId);
      TicketTypes = ticketTypeService.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPost()
    {
      try
      {
        ticketService.BookUserTickets(CreateDto, userService.GetCurrentUser(HttpContext).Id);
        return Redirect("./MyTicket");
      }
      catch (Exception exception)
      {
        ErrorMessage = exception.Message;
        InitData();
        return Page();
      }
    }
  }
}
