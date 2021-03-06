using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Authorization;

namespace Cinemo.Pages.Admin {
  public class IndexModel : PageModel {
    public IActionResult OnGet() {
      if(User.IsInRole("Admin"))
        return Redirect("Admin/Users");
      else
        return Redirect("Admin/Tickets");
    }
  }
}
