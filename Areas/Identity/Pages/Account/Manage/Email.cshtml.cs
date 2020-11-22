using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Areas.Identity.Pages.Account.Manage {
  public partial class EmailModel : PageModel {
    public IActionResult OnGet() {
      return RedirectToPage("Index");
    }
  }
}
