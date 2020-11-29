using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using Microsoft.AspNetCore.Http;

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
  }
}