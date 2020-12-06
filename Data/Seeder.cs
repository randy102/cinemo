using Cinemo.Models;
using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

public class SeedData
{
  public static void Seed(UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
  {
    SeedRoles(roleManager);

    SeedUsers(userManager);
  }

  public static async void SeedRoles(RoleManager<IdentityRole> roleManager)
  {
    var ROLE_LIST = new string[] { "Admin", "Member" };

    foreach (string role in ROLE_LIST)
    {
      if (!roleManager.RoleExistsAsync(role).Result)
      {
        IdentityRole iRole = new IdentityRole
        {
          Name = role,
          NormalizedName = role
        };
        await roleManager.CreateAsync(iRole);
      }
    }

  }

  public static async void SeedUsers(UserManager<User> userManager)
  {
    var USER_LIST = new[]{
      new { UserName = "admin@admin.com", Password = "Admin@123456", Role = "Admin", Id = "1"},
      new { UserName = "member@member.com", Password = "Member@123456", Role = "Member", Id = "2"}
    };

    foreach (var user in USER_LIST)
    {
      if (userManager.FindByEmailAsync(user.UserName).Result == null)
      {
        var iUser = new User
        {
          UserName = user.UserName,
          Email = user.UserName,
          Id = user.Id
        };
        IdentityResult result = userManager.CreateAsync(iUser, user.Password).Result;

        if (result.Succeeded)
        {
          await userManager.AddToRoleAsync(iUser, user.Role);
        }
      }
    }

  }
}