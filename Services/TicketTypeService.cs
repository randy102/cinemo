using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;
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

    public List<SelectListItem> GetSelectListItems(int defaultId = 0)
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Name.ToString(),
        Selected = c.Id == defaultId
      }).ToList();
    }

    public TicketType Delete(int id)
    {
      var existed = GetDetail(id);
      if(existed.Tickets.Any()) throw new Exception("Can not delete Ticket Type that already booked!");
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