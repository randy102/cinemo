using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.Ticket
{
  public class CreateModel : PageModel
  {
    private readonly ShowTimeService showTimeService;
    private readonly TicketTypeService ticketTypeService;
    private readonly UserService userService;
    private readonly TicketService ticketService;

    [BindProperty]
    public TicketCreateDto CreateDto { get; set; }

    [BindProperty]
    public string ErrorMessage { get; set; }

    public List<SelectListItem> ShowTimeSelectList { get; set; }
    public List<SelectListItem> TicketTypeSelectList { get; set; }
    public List<SelectListItem> UserSelectList { get; set; }

    public CreateModel(
      ShowTimeService showTimeService,
      TicketTypeService ticketTypeService,
      UserService userService,
      TicketService ticketService)
    {
      this.showTimeService = showTimeService;
      this.ticketService = ticketService;
      this.userService = userService;
      this.ticketTypeService = ticketTypeService;
    }

    private void GetInitData()
    {
      ShowTimeSelectList = showTimeService.GetShowingSelectListItems();
      TicketTypeSelectList = ticketTypeService.GetSelectListItems();
      UserSelectList = userService.GetSelectListItems();
    }

    public void OnGet()
    {
      GetInitData();
    }

    public IActionResult OnPost()
    {
      try{
        ticketService.CreateTicket(CreateDto);
        return Redirect("./");
      } catch(Exception error){
        ErrorMessage = error.Message;
        GetInitData();
        return Page();
      }
    }
  }
}
