using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.TicketType
{
  public class CreateModel : PageModel
  {
    private readonly TicketTypeService service;
    public CreateModel(TicketTypeService service)
    {
      this.service = service;
    }

    [BindProperty]
    public TicketTypeCreateDto CreateDto { get; set; }

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
