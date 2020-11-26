using System;
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
    public class CreateModel : PageModel
    {
        private readonly RoomService service;
        private readonly TheaterService theaterService;
        public CreateModel(RoomService service,TheaterService theaterService)
        {
        this.service = service;
        this.theaterService = theaterService;
        }
        [BindProperty]
        public RoomCreateDto CreateDto { get; set; }
        public List<SelectListItem> theaters { get; set; }

        public string ErrorMessage {get; set;}

        public void OnGet() { 
            theaters = theaterService.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Name
            }).ToList();
         }
        public IActionResult OnPost() {

            if (service.GetDetail(CreateDto.Name)!=null) {
                ErrorMessage = CreateDto.Name + " existed";

                return Page();
            }
            service.Create(CreateDto);

            return Redirect("/Admin/Rooms");
        }
    }
}
