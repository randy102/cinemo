
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
namespace Cinemo.Service
{
    public class RoomService
    {
        private RoomRepository repository;
        public RoomService(RoomRepository roomRepository)
        {
            this.repository = roomRepository;
        }

        public List<Room> GetAll()
        {
            return repository.FindAll();
        }

        public Room GetDetail(int id)
        {
            return repository.FindById(id);
        }

        public Room GetDetail(string name){
            name = FormatString.Trim_MultiSpaces_Title(name);
            return repository.FindAll().Where(r => r.Name.Equals(name)).FirstOrDefault();
        }

        public Room Delete(int id)
        {
            return repository.Delete(id);
        }

        public Room Create(RoomCreateDto dto)
        {
            var entity = new Room
            {
                Name = FormatString.Trim_MultiSpaces_Title(dto.Name),
                NumCol= dto.NumCol,
                NumRow= dto.NumRow,
                Total= dto.NumRow*dto.NumCol,
                Formats= dto.Formats
            };

            return repository.Add(entity);
        }

        public Room Update(RoomUpdateDto dto)
        {
            var entity = new Room
            {
                Id = dto.Id,
                Name = FormatString.Trim_MultiSpaces_Title(dto.Name),
                NumCol= dto.NumCol,
                NumRow= dto.NumRow,
                Total= dto.NumRow*dto.NumCol,
                Formats= dto.Formats
            };
            return repository.Update(entity);
        }
    }
}