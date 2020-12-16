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

    public List<Cinemo.Models.Movie> Movies { get; set; }
    public string ErrorMessage { get; set; }
    
    public IndexModel(MovieService service)
    {
      this.service = service;
    }

    public void InitData()
    {
      Movies = service.GetAll();
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
