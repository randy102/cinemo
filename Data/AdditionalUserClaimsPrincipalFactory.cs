using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;
using System.Threading.Tasks;
using Cinemo.Models;
using System.Security.Claims;
using Microsoft.Extensions.Options;

namespace Cinemo.Data {
  public class AdditionalUserClaimsPrincipalFactory
    : UserClaimsPrincipalFactory<User, IdentityRole> {
    public AdditionalUserClaimsPrincipalFactory(
      UserManager<User> userManager,
      RoleManager<IdentityRole> roleManager,
      IOptions<IdentityOptions> optionsAccessor)
      : base(userManager, roleManager, optionsAccessor) { }

    public async override Task<ClaimsPrincipal> CreateAsync(User user) {
      var principal = await base.CreateAsync(user);
      var identity = (ClaimsIdentity)principal.Identity;

      var claims = new List<Claim>();

      claims.Add(new Claim("FullName", user.FullName));

      identity.AddClaims(claims);
      return principal;
    }
  }
}
