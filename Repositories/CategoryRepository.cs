using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository
{
    public class CategoryRepository : EfCoreRepository<Category, ApplicationDbContext>
    {
        public CategoryRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}