using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;
using System;

namespace Cinemo.Pages.Admin.Theater
{
  public class CreateModel : PageModel
  {
    private readonly TheaterService service;

    [BindProperty]
    public TheaterCreateDto CreateDto { get; set; }

    [BindProperty]
    public string ErrorMessage { get; set; }

    public CreateModel(TheaterService service)
    {
      this.service = service;
    }

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
      try
      {
        service.Create(CreateDto);
        return Redirect("./");
      }
      catch (Exception error)
      {
        ErrorMessage = error.Message;
        return Page();
      }
    }
  }
}
