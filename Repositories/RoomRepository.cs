using Cinemo.Models;
using Cinemo.Data;

namespace Cinemo.Repository
{
    public class RoomRepository : EfCoreRepository<Room, ApplicationDbContext>
    {
        public RoomRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}