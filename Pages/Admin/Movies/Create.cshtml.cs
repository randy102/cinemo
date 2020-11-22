using System.Collections.Generic;
using System.Linq;
using Cinemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinemo.Service;

namespace Cinemo.Pages.Movie
{
  public class CreateModel : PageModel
  {
    private ApplicationDbContext db;
    private readonly MovieService service;

    public CreateModel(ApplicationDbContext db, MovieService service)
    {
      this.db = db;
      this.service = service;
    }

    [BindProperty]
    public MovieCreateDto CreateDto { get; set; }

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
      service.Create(CreateDto, getUser());
      return Redirect("./Index");
    }
  }
}
