
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;

namespace Cinemo.Service
{
    public class TicketTypeService
    {
        private TicketTypeRepository repository;
        public TicketTypeService(TicketTypeRepository ticketTypeRepository)
        {
            this.repository = ticketTypeRepository;
        }

        public List<TicketType> GetAll()
        {
            return repository.FindAll();
        }

        public TicketType GetDetail(int id)
        {
            return repository.FindById(id);
        }

        public TicketType Delete(int id)
        {
            return repository.Delete(id);
        }

        public TicketType Create(TicketTypeCreateDto dto)
        {
            var entity = new TicketType
            {
                Name = dto.Name,
                Price = dto.Price
            };

            return repository.Add(entity);
        }

        public TicketType Update(TicketTypeUpdateDto dto)
        {
            var entity = new TicketType
            {
                Id = dto.Id,
                Name = dto.Name,
                Price = dto.Price
            };
            return repository.Update(entity);
        }
    }
}