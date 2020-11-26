using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository {
  public class TheaterRepository : EfCoreRepository<Theater, ApplicationDbContext> {
    public TheaterRepository(ApplicationDbContext context) : base(context) {

    }
  }
}