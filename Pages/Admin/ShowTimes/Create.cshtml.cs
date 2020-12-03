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
    public class CreateModel : PageModel
    {
        private readonly ShowTimeService service;
        private readonly MovieService movieService;
        private readonly RoomService roomService;
        public CreateModel(ShowTimeService service, TheaterService theaterService, MovieService movieService, RoomService roomService)
        {
            this.service = service;
            this.movieService = movieService;
            this.roomService = roomService;
        }
        [BindProperty]
        public ShowTimeCreateDto CreateDto { get; set; }
        public List<SelectListItem> movies { get; set; }
        public List<SelectListItem> rooms { get; set; }
        [BindProperty]
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

        public void OnGet()
        {
            movies = getMovies();
            rooms = getRooms();
        }
        public IActionResult OnPost()
        {
            try
            {
                service.Create(CreateDto);
                return Redirect("./");
                // return Page();
            }
            catch (Exception error)
            {
                ErrorMessage = error.Message;
                movies = getMovies();
                rooms = getRooms();
                return Page();
            }
        }
    }
}

