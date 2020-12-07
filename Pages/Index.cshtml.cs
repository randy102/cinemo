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
    public IndexModel(ShowTimeService service)
    {
      this.service = service;
    }

    public List<Cinemo.Models.ShowTime> showingTimes { get; set; }
    public List<Cinemo.Models.Movie> notShowingTimes { get; set; }

    public void OnGet()
    {
      showingTimes=service.GetShowingTime();
      notShowingTimes=service.GetNotShowingTime();
    }
  }
}
