using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.Ticket
{
  public class IndexModel : PageModel
  {
    private readonly TicketService service;
    public IndexModel(TicketService service)
    {
      this.service = service;
    }
    public List<Cinemo.Models.Ticket> Tickets { get; set; }

    public void OnGet()
    {
      Tickets = service.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
      service.Delete(id);
      return Redirect("/Admin/Tickets");
    }
  }
}
