using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Http;
using Cinemo.Service;
namespace Cinemo.Pages
{
    public class MyTicketModel : PageModel
    {
        private readonly UserService userService;
        private readonly TicketService ticketService;
        public MyTicketModel(UserService userService, TicketService ticketService)
        {
            this.userService = userService;
            this.ticketService = ticketService;
        }
        public Cinemo.Models.User User { get; set; }
        public List<Cinemo.Models.Ticket> OldTickets { get; set; }
        public List<Cinemo.Models.Ticket> NowShowingTickets { get; set; }
        public List<Cinemo.Models.Ticket> UpComingTickets { get; set; }
        public string ErrorMessage { get; set; }

        public void InitData()
        {
            User = userService.GetCurrentUser(HttpContext);
            // -1 Old
            // 0 nowShowing
            // 1 upComing   
            OldTickets = ticketService.StatusTickets(User, -1);
            NowShowingTickets = ticketService.StatusTickets(User, 0);
            UpComingTickets = ticketService.StatusTickets(User, 1);
        }

        public void OnGet()
        {
            InitData();
        }

        public IActionResult OnPostDelete(int id)
        {
            try
            {
                ticketService.Delete(id);
                return Redirect("/MyTicket");
            }
            catch (Exception error)
            {
                ErrorMessage = error.Message;
                InitData();
                return Page();
            }
        }
    }
}
