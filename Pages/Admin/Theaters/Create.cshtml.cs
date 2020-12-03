using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.Theater
{
  public class CreateModel : PageModel
  {
    private readonly TheaterService service;
    public CreateModel(TheaterService service)
    {
      this.service = service;
    }

    [BindProperty]
    public TheaterCreateDto CreateDto { get; set; }

    [BindProperty]
    public string ErrorMessage {get; set;}

    public void OnGet()
    {
    }

    public IActionResult OnPost()
    {
        service.Create(CreateDto);
        return Redirect("./");
    }
  }
}
