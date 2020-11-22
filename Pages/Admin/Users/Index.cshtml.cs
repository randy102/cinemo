using System.Collections.Generic;
using System.Linq;
using Cinemo.Data;
using System.Threading.Tasks;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Identity;

namespace Cinemo.Pages.Admin {
  public class ListUserModel : PageModel {
    private UserManager<User> manager;
    public ListUserModel(UserManager<User> manager) => this.manager = manager;

    public List<User> Users { get; set; }

    public void OnGet() {
      var currentUser = User.Identity.Name;
      Users = manager.Users.Where(u => u.UserName != currentUser).ToList();
    }

    public async Task<IActionResult> OnPostChangeRoleAsync(string id) {
      var oldUser =  await manager.FindByIdAsync(id);
      var isAdmin = await manager.IsInRoleAsync(oldUser, "Admin");
      if(isAdmin){
        await manager.RemoveFromRoleAsync(oldUser, "Admin");
      } else {
        await manager.AddToRoleAsync(oldUser, "Admin");
      }
      return RedirectToPage();
    }

  }
}
