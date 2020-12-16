using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
using Microsoft.Extensions.Logging;

namespace Cinemo.Pages.Admin.Theater
{
  public class UpdateModel : PageModel
  {
    private readonly TheaterService service;
    public UpdateModel(TheaterService service)
    {
      this.service = service;
    }

    [BindProperty]
    public Cinemo.Models.Theater OldTheater { get; set; }

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty]
    public TheaterUpdateDto UpdateDto { get; set; }
    public string ErrorMessage { get; set; }
    public IActionResult OnGet()
    {
      OldTheater = service.GetDetail(id);
      if (OldTheater == null)
      {
        return Redirect("./");
      }
      else
      {
        return Page();
      }
    }

    public IActionResult OnPost()
    {
      try
      {
        service.Update(UpdateDto);
        return Redirect("./");
      }
      catch (Exception error)
      {
        ErrorMessage = error.Message;
        OldTheater = service.GetDetail(id);
        return Page();
      }
    }
  }
}
