using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Data;
using Cinemo.Service;

namespace Cinemo.Pages
{
  public class ListCategoryModel : PageModel
  {
    private readonly CategoryService service;

    public List<Category> Categories { get; set; }
    public string ErrorMessage { get; set; }
    
    public ListCategoryModel(CategoryService service)
    {
      this.service = service;
    }

    public void InitData()
    {
      Categories = service.GetAll();
    }

    public void OnGet()
    {
      InitData();
    }

    public IActionResult OnPostDelete(int id)
    {
      try
      {
        service.Delete(id);
        return RedirectToPage();
      }
      catch (Exception error)
      {
        InitData();
        ErrorMessage = error.Message;
        return Page();
      }
    }
  }
}
