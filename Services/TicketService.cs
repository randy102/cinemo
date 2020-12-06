using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;

namespace Cinemo.Service
{
  public class TicketService
  {
    private TicketRepository repository;
    public TicketService(TicketRepository ticketRepository)
    {
      this.repository = ticketRepository;
    }

    public List<Ticket> GetAll()
    {
      return repository.FindAll();
    }

    public Ticket Delete(int id)
    {
      return repository.Delete(id);
    }

  }
}