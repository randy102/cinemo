
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Models;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.Category
{
  public class CreateModel : PageModel
  {
    private readonly CategoryService service;
    public CreateModel(CategoryService service)
    {
      this.service = service;
    }

    [BindProperty]
    public CategoryCreateDto CreateDto { get; set; }

    public string ErrorMessage {get; set;}

    public IActionResult OnPost() {
      var isExist = service.GetDetail(CreateDto.Name);

      if (isExist!=null) {
        ErrorMessage = CreateDto.Name + " existed";

        return Page();
      }
      service.Create(CreateDto);

      return Redirect("/Admin/Categories");
    }
  }
}
