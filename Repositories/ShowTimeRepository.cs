using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository
{
    public class ShowTimeRepository : EfCoreRepository<ShowTime, ApplicationDbContext>
    {
        public ShowTimeRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}