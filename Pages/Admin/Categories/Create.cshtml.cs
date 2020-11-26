
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;
using System;

namespace Cinemo.Pages.Admin.Category
{
  public class CreateModel : PageModel
  {
    private readonly CategoryService service;
    public CreateModel(CategoryService service)
    {
      this.service = service;
    }

    [BindProperty]
    public CategoryCreateDto CreateDto { get; set; }

    public string ErrorMessage {get; set;}

    public IActionResult OnPost() {
      try{
        service.Create(CreateDto);
        return Redirect("./");
      } catch(Exception error){
        ErrorMessage = error.Message;
        return Page();
      }
    }
  }
}
