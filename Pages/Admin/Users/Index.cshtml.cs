using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Logging;

namespace Cinemo.Pages.Admin {
  public class ListUserModel : PageModel {
    private UserManager<User> manager;
    private readonly ILogger<ErrorModel> _logger;

    public ListUserModel(UserManager<User> manager, ILogger<ErrorModel> logger) {
      this.manager = manager;
      this._logger = logger;
    }

    public List<User> Users { get; set; }

    public void OnGet() {
      var currentUser = User.Identity.Name;
      Users = manager.Users.Where(u => u.UserName != currentUser).ToList();
    }


    public async Task<IActionResult> OnPostToggleAdminAsync(string id) {
      var oldUser =  await manager.FindByIdAsync(id);
      var isAdmin = await manager.IsInRoleAsync(oldUser, "Admin");
      if(isAdmin){
        await manager.RemoveFromRoleAsync(oldUser, "Admin");
      } else {
        await manager.AddToRoleAsync(oldUser, "Admin");
      }
      return RedirectToPage();
    }

    public async Task<IActionResult> OnPostToggleMemberAsync(string id) {
      var oldUser =  await manager.FindByIdAsync(id);
      var isMember = await manager.IsInRoleAsync(oldUser, "Member");
      if(isMember){
        await manager.RemoveFromRoleAsync(oldUser, "Member");
      } else {
        await manager.AddToRoleAsync(oldUser, "Member");
      }
      return RedirectToPage();
    }

  }
}
