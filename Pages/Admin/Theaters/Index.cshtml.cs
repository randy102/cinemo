using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Microsoft.AspNetCore.Mvc;

namespace Cinemo.Pages.Admin.Theater
{
  public class IndexModel : PageModel
  {
    private readonly TheaterService service;
    public IndexModel(TheaterService service)
    {
      this.service = service;
    
    }
    public List<Cinemo.Models.Theater> Theaters { get; set; }

    public void OnGet()
    {
      Theaters = service.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
      service.Delete(id);
      return Redirect("/Admin/Theaters");
    }
  }
}
