using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;

namespace Cinemo.Pages.Movie
{
  public class IndexModel : PageModel
  {
    private readonly MovieService service;
    public IndexModel(MovieService service)
    {
      this.service = service;
    }

    public List<Cinemo.Models.Movie> Movies { get; set; }

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
      Movies = service.GetAll();
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
        ErrorMessage = error.Message;
        return RedirectToPage(new { error = error.Message });
      }

    }
  }
}
