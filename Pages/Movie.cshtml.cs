using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
namespace Cinemo.Pages {
  public class MovieModel : PageModel {
    private readonly MovieService movieService;
    private readonly TheaterService theaterService;
    private readonly ShowTimeService showTimeService;

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty(SupportsGet = true)]
    public int theater { get; set; }

    [BindProperty(SupportsGet = true)]
    public string date { get; set; }

    public List<Theater> Theaters { get; set; }
    public Cinemo.Models.Movie Movie { get; set; }
    public List<ShowTime> ShowTimes { get; set; }

    public MovieModel(MovieService movieService, TheaterService theaterService, ShowTimeService showTimeService) {
      this.movieService = movieService;
      this.theaterService = theaterService;
      this.showTimeService = showTimeService;
    }

    public void InitData() {
      Movie = movieService.GetDetail(id);
      Theaters = theaterService.GetAll();
      ShowTimes = showTimeService.GetShowTimesByFilters(id, theater, date);
    }

    public void OnGet() {
      InitData();
    }
  }
}
