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
    public List<SelectListItem> MovieSelectList { get; set; }
    public List<SelectListItem> RoomSelectList { get; set; }
    [BindProperty]
    public string ErrorMessage { get; set; }

    public void OnGet()
    {
      MovieSelectList = movieService.GetSelectListItems();
      RoomSelectList = roomService.GetSelectListItems();
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
        MovieSelectList = movieService.GetSelectListItems(); ;
        RoomSelectList = roomService.GetSelectListItems();
        return Page();
      }
    }
  }
}

