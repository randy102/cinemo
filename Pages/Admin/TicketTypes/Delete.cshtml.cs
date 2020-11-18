using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.TicketTypes
{
    public class DeleteModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public DeleteModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        [BindProperty(SupportsGet = true)]
        public int id { get; set; } 
        public Cinemo.Models.TicketType ticketType { get; set; }
        public IActionResult OnGet()
        {
            ticketType = db.TicketTypes.Find(id);
            if (ticketType == null)
            {
                return Redirect("./");
            }
            else
            {
                return Page();
            }
        }
        public IActionResult OnPost()
        {
            ticketType = db.TicketTypes.Find(id);
            db.Remove(ticketType);
            db.SaveChanges();
            return Redirect("./");
        }
    }
}
