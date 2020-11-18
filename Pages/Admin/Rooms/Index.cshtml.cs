using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.Room
{
    public class IndexModel : PageModel
    {       
        private Cinemo.Data.ApplicationDbContext db;
        public IndexModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        public List<Cinemo.Models.Room> rooms { get; set; }
        public void OnGet()
        {
            rooms=db.Rooms.ToList();
        }
    }
}
