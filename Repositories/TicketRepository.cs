using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository
{
  public class TicketRepository : EfCoreRepository<Ticket, ApplicationDbContext>
  {
    public TicketRepository(ApplicationDbContext context) : base(context)
    {

    }
  }
}