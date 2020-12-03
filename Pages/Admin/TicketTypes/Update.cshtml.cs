using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
using Microsoft.Extensions.Logging;

namespace Cinemo.Pages.Admin.TicketType
{
  public class UpdateModel : PageModel
  {
    private readonly TicketTypeService service;
    public UpdateModel(TicketTypeService service)
    {
      this.service = service;
    }

    [BindProperty]
    public Cinemo.Models.TicketType OldTicketType { get; set; }

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty]
    public TicketTypeUpdateDto TicketType {get; set;}

    public IActionResult OnGet()
    {
      OldTicketType = service.GetDetail(id);
      if (OldTicketType == null) {
        return Redirect("./");
      } else {
        return Page();
      }
    }

    public IActionResult OnPost()
    {
      service.Update(TicketType);
      return Redirect("./");
    }
  }
}
