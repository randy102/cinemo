using System;
using Cinemo.Models;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cinemo.Pages.Admin.Room
{
    public class UpdateModel : PageModel
    {
        private readonly RoomService service;
    private readonly TheaterService theaterService;
        public UpdateModel(RoomService service,TheaterService theaterService)
        {
        this.service = service;
        this.theaterService = theaterService;
        }
    [BindProperty]
    public Cinemo.Models.Room OldRoom { get; set; }

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty]
    public RoomUpdateDto UpdateDto {get; set;}
    public List<SelectListItem> theaters { get; set; }
    public string ErrorMessage {get; set;}
    public List<SelectListItem> getTheaters()
        {
            var theaters = theaterService.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
            return theaters;
        }
    public IActionResult OnGet()
    {
      theaters = getTheaters();
      OldRoom = service.GetDetail(id);
      if (OldRoom == null) {
        return Redirect("./");
      } else {
        return Page();
      }
    }
    public IActionResult OnPost()
    {
      try{
        service.Update(UpdateDto);
        return Redirect("./");
      } catch(Exception error){
        ErrorMessage = error.Message;
        theaters = getTheaters();
        OldRoom = service.GetDetail(id);
        return Page();
      }
    }
    }
}
