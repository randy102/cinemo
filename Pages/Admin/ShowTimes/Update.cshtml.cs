using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cinemo.Pages.Admin.ShowTime
{
    public class UpdateModel : PageModel
    {
        private readonly ShowTimeService service;
        // private readonly TheaterService theaterService;
        private readonly MovieService movieService;
        private readonly RoomService roomService;
        public UpdateModel(ShowTimeService service, TheaterService theaterService, MovieService movieService, RoomService roomService)
        {
            this.service = service;
            // this.theaterService = theaterService;
            this.movieService = movieService;
            this.roomService = roomService;
        }
        [BindProperty]
    public Cinemo.Models.ShowTime OldShowTime { get; set; }
    [BindProperty(SupportsGet = true)]
    public int id { get; set; }
        [BindProperty]
        public ShowTimeUpdateDto UpdateDto { get; set; }
        public List<SelectListItem> movies { get; set; }
        public List<SelectListItem> rooms { get; set; }

        public string ErrorMessage { get; set; }
        public List<SelectListItem> getMovies()
        {
            var movies = movieService.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Title
            }).ToList();
            return movies;
        }
        public List<SelectListItem> getRooms()
        {
            var rooms = roomService.GetAll().Select(c => new SelectListItem
            {
                Value = c.Id.ToString(),
                Text = c.Theater.Name+"/ "+c.Name+"("+c.Formats+")"
            }).ToList();
            return rooms;
        }

        public IActionResult OnGet()
        {
            movies = getMovies();
            rooms = getRooms();
            OldShowTime = service.GetDetail(id);
            if (OldShowTime == null) {
                return Redirect("./");
            } else {
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
                movies = getMovies();
                rooms = getRooms();
                // rooms = roomService.GetAll(
                OldShowTime = service.GetDetail(id);
                return Page();
            }
        }
    }
}

