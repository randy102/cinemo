using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;

namespace Cinemo.Pages
{
  public class CreatePostModel : PageModel
  {
    private UserManager<User> manager;
    private ApplicationDbContext db;
    public CreatePostModel(ApplicationDbContext db, UserManager<User> manager)
    {
      this.db = db;
      this.manager = manager;
    }

    [BindProperty]
    public string title { get; set; }
    [BindProperty]
    public string content { get; set; }
    [BindProperty]
    public string categoryId { get; set; }
    [BindProperty]
    public string tags { get; set; }
    public List<SelectListItem> categories { get; set; }


    public User getUser()
    {
      var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
    }
    
    public void OnGet()
    {
      //Tạo list category
      categories = db.Categories.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Name
      }).ToList();
    }

    public IActionResult OnPost()
    {
      if (!ModelState.IsValid)
        return Page();

      var author = getUser();
      var post = new Movie();
      post.AuthorId = author.Id;
      post.Author = author;
      post.Title = title;
      post.Content = content;
      post.Tags = tags;
      post.CategoryId = Int16.Parse(categoryId);
      post.Category = db.Categories.Find(Int32.Parse(categoryId));
      post.CreatedAt = System.DateTime.Now;

      db.Movies.Add(post);
      db.SaveChanges();

      return Redirect("./Index");
    }
  }
}
