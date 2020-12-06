using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Cinemo.Data;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.AspNetCore.Authorization;
using Cinemo.Models;
using Cinemo.Repository;
using Cinemo.Service;

namespace Cinemo
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {
      services.AddDbContext<ApplicationDbContext>(options =>
        options.UseSqlite(
          Configuration.GetConnectionString("DefaultConnection")));

      services.AddDatabaseDeveloperPageExceptionFilter();

      services.AddDefaultIdentity<User>()
        .AddRoles<IdentityRole>()
        .AddEntityFrameworkStores<ApplicationDbContext>();

      services.AddRazorPages(options =>
      {
        options.Conventions
        .AuthorizeFolder("/Admin", "OnlyStaff")
        .AuthorizeFolder("/Admin/Users", "OnlyAdmin")
        .AuthorizeFolder("/Admin/Theaters", "OnlyAdmin")
        .AuthorizeFolder("/Admin/Rooms", "OnlyAdmin");
      });

      services.AddAuthorization(options =>
      {
        options.FallbackPolicy = new AuthorizationPolicyBuilder()
          .RequireAuthenticatedUser()
          .Build();
        options.AddPolicy("OnlyStaff", policy => policy.RequireRole("Admin","Member"));
        options.AddPolicy("OnlyAdmin", policy => policy.RequireRole("Admin"));
      });



      services
        .AddScoped<UserService>()

        .AddScoped<FileService>()

        .AddScoped<TheaterRepository>()
        .AddScoped<TheaterService>()

        .AddScoped<MovieRepository>()
        .AddScoped<MovieService>()

        .AddScoped<RoomRepository>()
        .AddScoped<RoomService>()

        .AddScoped<TicketTypeRepository>()
        .AddScoped<TicketTypeService>()

        .AddScoped<CategoryRepository>()
        .AddScoped<CategoryService>()

        .AddScoped<ShowTimeRepository>()
        .AddScoped<ShowTimeService>()
        
        .AddScoped<TicketRepository>()
        .AddScoped<TicketService>();

    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
        app.UseMigrationsEndPoint();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
        app.UseHsts();
      }

      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      SeedData.Seed(userManager, roleManager);

      app.UseEndpoints(endpoints =>
      {
        endpoints.MapRazorPages();
      });
    }
  }
}
