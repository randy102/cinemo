using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Cinemo.Service;

namespace Cinemo.Pages.Admin.TicketType
{
    public class IndexModel : PageModel
    {
        private readonly TicketTypeService service;
        public IndexModel(TicketTypeService service)
        {
            this.service = service;
        }

        [BindProperty]
        public int id { get; set; } 
        public List<Cinemo.Models.TicketType> TicketTypes { get; set; }

        public void OnGet()
        {
            TicketTypes = service.GetAll();
        }

        public IActionResult OnPostDelete()
        {
            service.Delete(id);
            return Redirect("/Admin/TicketTypes");
        }
    }
}
