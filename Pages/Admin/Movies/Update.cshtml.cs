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
    private readonly MovieService service;
    private readonly CategoryService categoryService;

    public UpdateModel( MovieService service, CategoryService categoryService)
    {
      this.service = service;
      this.categoryService = categoryService;
    }

    [BindProperty]
    public MovieUpdateDto UpdateDto { get; set; }

    [BindProperty]
    public Cinemo.Models.Movie OldMovie { get; set; }


    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    public List<SelectListItem> Categories { get; set; }

    public IActionResult OnGet()
    {
      OldMovie = service.GetDetail(id);
      if (OldMovie == null)
        return Redirect("./");

      Categories = categoryService.GetSelectListItems(OldMovie.CategoryId);
      return Page();
    }

    public IActionResult OnPost()
    {
      service.Update(UpdateDto);
      return Redirect("./");
    }
  }
}
