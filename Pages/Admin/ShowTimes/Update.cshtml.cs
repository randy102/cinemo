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
    private readonly MovieService movieService;
    private readonly RoomService roomService;

    [BindProperty]
    public Cinemo.Models.ShowTime OldShowTime { get; set; }
    
    [BindProperty(SupportsGet = true)]
    public int id { get; set; }
    
    [BindProperty]
    public ShowTimeUpdateDto UpdateDto { get; set; }

    public List<SelectListItem> movies { get; set; }
    public List<SelectListItem> rooms { get; set; }

    public string ErrorMessage { get; set; }

    public UpdateModel(ShowTimeService service, MovieService movieService, RoomService roomService)
    {
      this.service = service;
      this.movieService = movieService;
      this.roomService = roomService;
    }

    private void getInitData()
    {
      OldShowTime = service.GetDetail(id);
      movies = movieService.GetSelectListItems(OldShowTime.MovieId);
      rooms = roomService.GetSelectListItems(OldShowTime.RoomId);
    }

    public IActionResult OnGet()
    {
      getInitData();
      return Page();

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
        getInitData();
        return Page();
      }
    }
  }
}

