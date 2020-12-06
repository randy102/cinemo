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
    private readonly MovieService service;
    private readonly CategoryService categoryService;
    private readonly UserService userService;

    public CreateModel(MovieService service, CategoryService categoryService, UserService userService)
    {
      this.service = service;
      this.categoryService = categoryService;
      this.userService = userService;
    }

    [BindProperty]
    public MovieCreateDto CreateDto { get; set; }

    public List<SelectListItem> CategorySelectList { get; set; }

    public void OnGet()
    {
      CategorySelectList = categoryService.GetSelectListItems();
    }

    public IActionResult OnPost()
    {
      service.Create(CreateDto);
      return Redirect("./Index");
    }
  }
}
