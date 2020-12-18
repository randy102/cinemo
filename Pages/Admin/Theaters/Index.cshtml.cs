using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Microsoft.AspNetCore.Mvc;
using System;

namespace Cinemo.Pages.Admin.Theater
{
  public class IndexModel : PageModel
  {
    private readonly TheaterService service;

    [BindProperty]
    public string ErrorMessage {get; set;}

    public List<Cinemo.Models.Theater> Theaters { get; set; }
    
    public IndexModel(TheaterService service)
    {
      this.service = service;
    
    }

    public void InitData()
    {
      Theaters = service.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPostDelete(int id)
    {
      try{
        service.Delete(id);
        return Redirect("/Admin/Theaters");
      } catch(Exception exception){
        InitData();
        ErrorMessage = exception.Message;
        return Page();
      }
      
    }
  }
}
