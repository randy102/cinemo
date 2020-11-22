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
  public class UpdateModel : PageModel
  {
    private ApplicationDbContext db;
    private readonly MovieService service;

    public UpdateModel(ApplicationDbContext db, MovieService service)
    {
      this.db = db;
      this.service = service;
    }

    [BindProperty]
    public MovieUpdateDto UpdateDto { get; set; }

    [BindProperty]
    public Cinemo.Models.Movie OldMovie { get; set; }


    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    public List<SelectListItem> Categories { get; set; }

    public User getUser()
    {
      var email = HttpContext.User.FindFirst(ClaimTypes.Email).Value;
      return db.Users.Where(u => u.Email.Equals(email)).FirstOrDefault();
    }

    public IActionResult OnGet()
    {
      //Tạo list category
      Categories = db.Categories.Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Name
      }).ToList();

      OldMovie = service.GetDetail(id);
      if (OldMovie == null)
        return Redirect("./");
      return Page();
    }
  }
}
