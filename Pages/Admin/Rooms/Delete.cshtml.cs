using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.Room
{
    public class DeleteModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public DeleteModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        public Cinemo.Models.Room room { get; set;}
        public IActionResult OnGet() {
            room = db.Rooms.Find(id);
            if (room == null) {
                return Redirect("./");
            } else {
                return Page();
            }
        }
        public IActionResult OnPost() {
            room = db.Rooms.Find(id);
            db.Remove(room);
            db.SaveChanges();
            return Redirect("./");
        }
    }
}
