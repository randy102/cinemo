using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.Room
{
    public class CreateModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public CreateModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public int numCol { get; set; }
        [BindProperty]
        public int numRow { get; set; }
        [BindProperty]
        public string formats { get; set; }
        public string errMsg { get; set; }="init";
        public void OnGet(){}
        public IActionResult OnPost()
        {
            name=Cinemo.Utils.FormatString.Trim_MultiSpaces_Title(name);
            bool isExist = (db.Rooms.Where(r => r.Name.Equals(name))).ToList().Any();
            if(!isExist){
                var room=new Cinemo.Models.Room{
                    Name=name,
                    NumCol=numCol,
                    NumRow=numRow,
                    Total=numCol*numRow,
                    Formats=formats
                };
                db.Rooms.Add(room);
                db.SaveChanges();
                return Redirect("./");
            }
            else
            {
                errMsg="Exist";
                return Page();
            }
        }
    }
}
