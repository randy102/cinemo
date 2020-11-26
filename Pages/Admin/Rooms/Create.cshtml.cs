using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
namespace Cinemo.Pages.Admin.Room {
  public class CreateModel : PageModel {
    private readonly RoomService service;
    public CreateModel(RoomService service) {
      this.service = service;
    }

    [BindProperty]
    public RoomCreateDto CreateDto { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {

      if (service.GetDetail(CreateDto.Name) != null) {
        ErrorMessage = CreateDto.Name + " existed";

        return Page();
      }
      service.Create(CreateDto);

      return Redirect("/Admin/Rooms");
    }
  }
}
