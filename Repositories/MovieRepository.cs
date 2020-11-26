using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository {
  public class MovieRepository : EfCoreRepository<Movie, ApplicationDbContext> {
    public MovieRepository(ApplicationDbContext context) : base(context) {

    }
  }
}