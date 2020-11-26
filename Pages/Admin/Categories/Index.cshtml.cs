using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Data;
using Cinemo.Service;

namespace Cinemo.Pages {
  public class ListCategoryModel : PageModel {
    private readonly CategoryService service;
    public ListCategoryModel(CategoryService service)
    {
      this.service = service;
    }

    public List<Category> Categories { get; set; }

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }

    public IActionResult OnGet() {
      Categories = service.GetAll();
      return Page();
    }

    public IActionResult OnPostDelete(int id) {
      try {
        var category =service.GetDetail(id);
        var postsOfCate = category.Movies;

        if (postsOfCate.Any())
          throw new Exception("Category has been used!");

        service.Delete(id);

        return RedirectToPage();
      }
      catch (Exception error) {
        ErrorMessage = error.Message;
        return RedirectToPage(new { error = error.Message });
      }
    }
  }
}
