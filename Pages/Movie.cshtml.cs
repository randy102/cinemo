using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
namespace Cinemo.Pages
{
  public class MovieModel : PageModel
  {
    private readonly MovieService movieService;
    private readonly TheaterService theaterService;

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    public List<Theater> Theaters {get; set;}
    public Cinemo.Models.Movie Movie {get; set;}

    public MovieModel(MovieService movieService, TheaterService theaterService)
    {
      this.movieService = movieService;
      this.theaterService = theaterService;
    }

    public void InitData(){
      Movie = movieService.GetDetail(id);
      Theaters = theaterService.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }
  }
}
