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
    public IndexModel(RoomService service)
    {
      this.service = service;
    }

    [BindProperty]
    public int id { get; set; }

    public List<Cinemo.Models.Room> Rooms { get; set; }

    public void OnGet()
    {
      Rooms = service.GetAll();
    }

    public IActionResult OnPostDelete()
    {
      service.Delete(id);
      return Redirect("/Admin/Rooms");
    }
    }
}
