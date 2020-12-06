using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.Rendering;
namespace Cinemo.Service
{
  public class UserService
  {
    private UserManager<User> manager;
    public UserService(UserManager<User> manager) => this.manager = manager;

    public User GetCurrentUser(HttpContext context){
      var userName = context.User.Identity.Name;
      return manager.FindByNameAsync(userName).Result;
    }

    public List<User> GetAll(){
      return manager.Users.ToList();
    }

    public List<SelectListItem> GetSelectListItems(string defaultId = null)
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id,
        Text = c.Email,
        Selected = c.Id == defaultId
      }).ToList();
    }
  }
}