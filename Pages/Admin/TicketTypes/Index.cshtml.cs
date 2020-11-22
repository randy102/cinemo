using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.TicketType
{
    public class IndexModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public IndexModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;

        [BindProperty]
        public int id { get; set; } 
        public List<Cinemo.Models.TicketType> ticketTypes { get; set; }

        public void OnGet()
        {
            ticketTypes=db.TicketTypes.ToList();
        }

        public IActionResult OnPostDelete()
        {
           var ticketType = db.TicketTypes.Find(id);
            db.Remove(ticketType);
            db.SaveChanges();
            return Redirect("/Admin/TicketTypes");
        }
    }
}
