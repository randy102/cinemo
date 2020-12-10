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

    public string ErrorMessage { get; set; }

    public List<Cinemo.Models.ShowTime> showTimes { get; set; }

    public IndexModel(ShowTimeService service)
    {
      this.service = service;
    }

    public void InitData()
    {
      showTimes = service.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPostDelete(int id)
    {
      try {
        service.Delete(id);
        return RedirectToPage();
      } catch (Exception exception){
        ErrorMessage = exception.Message;
        InitData();
        return Page();
      }
    }

    public IActionResult OnPostChangeStatus(int id)
    {
      try {
        service.ChangeStatus(id);
        return Redirect("/Admin/ShowTimes");
      } catch (Exception exception){
        ErrorMessage = exception.Message;
        InitData();
        return Page();
      }
    }
  }
}
