using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;
using Cinemo.Models;
using Microsoft.Extensions.Logging;

namespace Cinemo.Pages.Admin.Category
{
  public class UpdateModel : PageModel
  {
    private readonly CategoryService service;
    public UpdateModel(CategoryService service)
    {
      this.service = service;
    }

    [BindProperty]
    public Cinemo.Models.Category OldCategory { get; set; }

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty]
    public CategoryUpdateDto Category {get; set;}
    public string ErrorMessage {get; set;}

    public IActionResult OnGet()
    {
      OldCategory = service.GetDetail(id);
      if (OldCategory == null) {
        return Redirect("./");
      } else {
        return Page();
      }
    }

    public IActionResult OnPost()
    {
      if (service.GetDetail(Category.Name)!=null) {
        ErrorMessage = Category.Name + " existed";

        return Page();
      }
      service.Update(Category);
      return Redirect("./");
    }
  }
}
