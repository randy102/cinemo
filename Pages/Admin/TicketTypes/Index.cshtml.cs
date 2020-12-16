using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using System;

namespace Cinemo.Pages.Admin.TicketType
{
  public class IndexModel : PageModel
  {
    private readonly TicketTypeService service;
    public List<Cinemo.Models.TicketType> TicketTypes { get; set; }
    
    public string ErrorMessage {get; set;}
    
    public IndexModel(TicketTypeService service)
    {
      this.service = service;
    }

    public void InitData()
    {
        TicketTypes = service.GetAll();
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
        return RedirectToPage();
      }
      catch (Exception error)
      {
        InitData();
        ErrorMessage = error.Message;
        return Page();
      }
    }
  }
}
