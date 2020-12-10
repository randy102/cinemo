using Microsoft.AspNetCore.Identity;
using System.Collections.Generic;

namespace Cinemo.Models {
  public class User : IdentityUser {
    public string FullName { get; set; }

    public virtual List<Ticket> Tickets {get; set;}
  }
}