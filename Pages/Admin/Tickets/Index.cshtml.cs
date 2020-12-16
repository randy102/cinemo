using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using System;
namespace Cinemo.Pages.Admin.Ticket
{
  public class IndexModel : PageModel
  {
    private readonly TicketService service;

    public string ErrorMessage { get; set; }
    public List<Cinemo.Models.Ticket> Tickets { get; set; }

    public IndexModel(TicketService service)
    {
      this.service = service;
    }

    public void InitData()
    {
      Tickets = service.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPostDelete(int id)
    {
      try
      {
        service.Delete(id);
        return Redirect("/Admin/Tickets");
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
