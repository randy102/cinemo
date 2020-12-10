using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages
{
  public class BookTicketModel : PageModel
  {
    [BindProperty(SupportsGet = true)]
    public int showtime { get; set; }
    public void OnGet()
    {
    }
  }
}
