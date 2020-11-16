using Cinemo.Models;
using Microsoft.AspNetCore.Identity;
using System;
using Cinemo.Data;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using System.Linq;

public class SeedData
{
  public static async void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
  {
    if (!roleManager.RoleExistsAsync("Admin").Result)
    {
      IdentityRole role = new IdentityRole();
      role.Name = "Admin";
      role.NormalizedName = "Admin";
      await roleManager.CreateAsync(role);
    }

    if (userManager.FindByEmailAsync("admin@admin.com").Result == null)
    {
      var user = new User
      {
        UserName = "admin@admin.com",
        Email = "admin@admin.com"
      };
      IdentityResult result = userManager.CreateAsync(user, "Admin@123456").Result;

      if (result.Succeeded)
      {
        await userManager.AddToRoleAsync(user,"Admin");
      }
    }

  }
}