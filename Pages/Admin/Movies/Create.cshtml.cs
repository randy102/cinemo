using System.Collections.Generic;
using System.Linq;
using Cinemo.Data;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Security.Claims;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinemo.Service;
using System;

namespace Cinemo.Pages.Movie
{
  public class CreateModel : PageModel
  {
    private readonly MovieService service;
    private readonly CategoryService categoryService;
    private readonly UserService userService;

    [BindProperty]
    public MovieCreateDto CreateDto { get; set; }

    public List<SelectListItem> CategorySelectList { get; set; }
    public string ErrorMessage {get; set;}

    public CreateModel(MovieService service, CategoryService categoryService, UserService userService)
    {
      this.service = service;
      this.categoryService = categoryService;
      this.userService = userService;
    }

    public void InitData(){
      CategorySelectList = categoryService.GetSelectListItems();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPost()
    {
      try
      {
        service.Create(CreateDto);
        return Redirect("./Index");
      }
      catch (Exception exception)
      {
        InitData();
        ErrorMessage = exception.Message;
        return Page();
      }
    }
  }
}
