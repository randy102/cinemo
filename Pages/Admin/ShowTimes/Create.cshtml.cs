using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.ShowTime {
  public class CreateModel : PageModel {
    public string ErrorMessage;

    public Models.Room[] rooms;

    public Models.Theater[] theaters;

    public void OnGet() {

    }
  }
}
