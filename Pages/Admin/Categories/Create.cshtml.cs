using System;
using System.Linq;
using Cinemo.Data;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Utils;
using System.ComponentModel.DataAnnotations;

namespace Cinemo.Pages {
  public class CreateCategoryModel : PageModel {
    private ApplicationDbContext db;

    public CreateCategoryModel(ApplicationDbContext db) => this.db = db;

    [BindProperty]
    public string newName { get; set; }

    public string ErrorMessage { get; set; }

    public IActionResult OnPost() {
      if (!ModelState.IsValid)
        return Page();

      newName = FormatString.Trim_MultiSpaces_Title(newName);

      bool isExist = (db.Categories.Where(c => c.Name.Equals(newName))).ToList().Any();

      if (isExist) {
        ErrorMessage = newName + " existed";

        return Page();
      }

      Category category = new Category();

      category.Name = newName;

      db.Categories.Add(category);

      db.SaveChanges();

      return Redirect("/Admin/Categories");
    }
  }
}
