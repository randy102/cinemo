using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.TicketType
{
    public class UpdateModel : PageModel
    {
        
        private Cinemo.Data.ApplicationDbContext db;
        public UpdateModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        public string errMsg { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        [BindProperty]
        public Cinemo.Models.TicketType.Type name { get; set; }
        [BindProperty]
        public float price { get; set; }
        public Cinemo.Models.TicketType ticketType { get; set; }
    public IActionResult OnGet() {
      ticketType = db.TicketTypes.Find(id);
      if (ticketType == null) {
        return Redirect("./");
      } else {
        return Page();
      }
    }
    public IActionResult OnPost() {
      ticketType=db.TicketTypes.Find(id);
      ticketType.Name=name;
      ticketType.Price=price;
      db.Update(ticketType);
        //Lưu thay đổi 
        db.SaveChanges();
        //Chuyển hướng đến trang Listroom
        return Redirect("./");
    }
}}
