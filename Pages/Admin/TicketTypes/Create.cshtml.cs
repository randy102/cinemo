using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.TicketType
{
    public class CreateModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public CreateModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        [BindProperty]
        public Cinemo.Models.TicketType.Type name { get; set; }
        [BindProperty]
        public float price { get; set; }
        public void OnGet(){}
        public IActionResult OnPost()
        {
            var ticketType=new Cinemo.Models.TicketType{
                    Name=name,
                    Price=price
                };
                db.TicketTypes.Add(ticketType);
                db.SaveChanges();
                return Redirect("./");
        }
    }
}
