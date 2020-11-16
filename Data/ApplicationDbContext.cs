using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Cinemo.Models;
using Microsoft.AspNetCore.Identity;

namespace Cinemo.Data
{
  public class ApplicationDbContext : IdentityDbContext<User>
  {
    public override DbSet<User> Users {get; set;}
    public DbSet<Category> Categories { get; set; }
    public DbSet<Movie> Movies { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
      optionsBuilder.UseLazyLoadingProxies();
    }
  }
}
