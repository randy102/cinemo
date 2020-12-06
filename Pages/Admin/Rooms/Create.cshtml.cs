using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinemo.Models;
namespace Cinemo.Pages.Admin.Room
{
  public class CreateModel : PageModel
  {
    private readonly RoomService service;
    private readonly TheaterService theaterService;

    [BindProperty]
    public RoomCreateDto CreateDto { get; set; }
    public List<SelectListItem> theaters { get; set; }

    public string ErrorMessage { get; set; }

    public CreateModel(RoomService service, TheaterService theaterService)
    {
      this.service = service;
      this.theaterService = theaterService;
    }
    

    public void OnGet()
    {
      theaters = theaterService.GetSelectListItems();
    }

    public IActionResult OnPost()
    {

      if (service.GetDetail(CreateDto.TheaterId, CreateDto.Name) != null)
      {
        ErrorMessage = CreateDto.Name + " existed";

        return Page();
      }
      service.Create(CreateDto);

      return Redirect("/Admin/Rooms");
    }
  }
}
