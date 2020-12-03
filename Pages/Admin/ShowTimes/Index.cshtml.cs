using System;
using System.Collections.Generic;
using System.Linq;
using Cinemo.Service;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.ShowTime
{
    public class IndexModel : PageModel
    {
        private readonly ShowTimeService service;
    public IndexModel(ShowTimeService service)
    {
      this.service = service;
    }

    public List<Cinemo.Models.ShowTime> showTimes { get; set; }

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }
    public void OnGet()
    {
      showTimes = service.GetAll();
    }

    public IActionResult OnPostDelete(int id)
    {
      service.Delete(id);
      return Redirect("/Admin/ShowTimes");
    }

    public IActionResult OnPostChangeStatus(int id)
    {
      service.ChangeStatus(service.GetDetail(id));
      return Redirect("/Admin/ShowTimes");
    }
    }
}
