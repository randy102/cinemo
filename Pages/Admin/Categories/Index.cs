using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Pages {
  public class ListCategoryModel : PageModel {
    private ApplicationDbContext db;
    public ListCategoryModel(ApplicationDbContext db) => this.db = db;

    public List<Category> Categories { get; set; }

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }

    public IActionResult OnGet() {
      Categories = db.Categories.ToList();
      return Page();
    }

    public IActionResult OnPostDelete(int id) {
      try{
        var category = db.Categories.Find(id);
        var postsOfCate = db.Movies.Where(post => post.CategoryId == id).ToList();

        if(postsOfCate.Any())
          throw new Exception("Category has been used!");

        db.Categories.Remove(category);
        db.SaveChanges();

        return RedirectToPage();
      } catch(Exception error){
        ErrorMessage = error.Message;
        return RedirectToPage(new {error = error.Message});
      }
    }
  }
}
