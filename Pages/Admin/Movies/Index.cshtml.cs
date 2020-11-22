using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Data;
using Cinemo.Models;
using System.Security.Claims;

namespace Cinemo.Pages {
  public class ListPostModel : PageModel {
    private ApplicationDbContext db;
    public ListPostModel(ApplicationDbContext db) => this.db = db;

    public List<Movie> Movies { get; set; }

    [BindProperty(Name = "error", SupportsGet = true)]
    public string ErrorMessage { get; set; }

    public void OnGet() {
      Movies = db.Movies.ToList();
    }

    public IActionResult OnPostDelete(int id) {
      try{
        var post = db.Movies.Find(id);

        // Todo: Check if movie is used in showtime

        db.Movies.Remove(post);
        db.SaveChanges();
        return RedirectToPage();
      } 
      catch(Exception error){
        ErrorMessage = error.Message;
        return RedirectToPage(new {error = error.Message});
      } 
      
    }
  }
}
