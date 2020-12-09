// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.AspNetCore.Mvc.RazorPages;
// using Microsoft.Extensions.Logging;
// using Microsoft.AspNetCore.Authorization;
// using Microsoft.AspNetCore.Identity;
// using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

// namespace Cinemo.Pages
// {
//   [AllowAnonymous]
//   public class IndexModel : PageModel
//   {

//     public void OnGet()
//     {
//     }
//   }
// }
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Cinemo.Service;
namespace Cinemo.Pages
{
  [AllowAnonymous]
  public class IndexModel : PageModel
  {
    private readonly ShowTimeService service;
    private readonly MovieService movieService;

    public IndexModel(ShowTimeService service, MovieService movieService)
    {
      this.service = service;
      this.movieService = movieService;
    }

    public List<Cinemo.Models.Movie> ShowingMovies { get; set; }
    public List<Cinemo.Models.Movie> UpcomingMovies { get; set; }

    public void InitData()
    {
      ShowingMovies = movieService.GetShowingMovies();
      UpcomingMovies = movieService.GetUpcomingMovies();
    }

    public void OnGet()
    {
      InitData();
    }
  }
}
