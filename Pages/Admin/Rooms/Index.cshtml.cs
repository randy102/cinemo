using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Cinemo.Service;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.Room
{
  public class IndexModel : PageModel
  {
    private readonly RoomService service;

    public List<Cinemo.Models.Room> Rooms { get; set; }
    public string ErrorMessage { get; set; }

    public IndexModel(RoomService service)
    {
      this.service = service;
    }

    public void InitData()
    {
      Rooms = service.GetAll();
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
