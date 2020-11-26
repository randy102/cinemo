
using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository {
  public class TicketTypeRepository : EfCoreRepository<TicketType, ApplicationDbContext> {
    public TicketTypeRepository(ApplicationDbContext context) : base(context) {

    }
  }
}