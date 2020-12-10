using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using System.Linq;
using System;
namespace Cinemo.Service
{
  public class TicketService
  {
    private TicketRepository repository;
    private ShowTimeRepository showTimeRepository;

    public TicketService(TicketRepository ticketRepository, ShowTimeRepository showTimeRepository)
    {
      this.repository = ticketRepository;
      this.showTimeRepository = showTimeRepository;
    }

    public List<Ticket> GetAll()
    {
      return repository.FindAll();
    }

    public Ticket Delete(int id)
    {
      return repository.Delete(id);
    }

    public void CheckDupplicatedSeat(string Seat, int ShowTimeId)
    {
      bool isSeatExisted = repository.FindWhere(t => t.Seat == Seat && t.ShowTimeId == ShowTimeId).Any();
      if(isSeatExisted) throw new Exception("Seat has been booked!");
    }

    private Tuple<int, int> ParseSeat(string Seat)
    {
      var parsedArr = Seat.Split('-').ToArray();
      char[] cRow = parsedArr[0].ToCharArray();
      int row = (int)cRow[0] - 64;
      int col = Int32.Parse(parsedArr[1]);
      return new Tuple<int, int>(row, col);
    }

    public void CheckSeatInRange(string Seat, int ShowTimeId)
    {
      ShowTime showTime = showTimeRepository.FindById(ShowTimeId);
      int numCol = showTime.Room.NumCol;
      int numRow = showTime.Room.NumRow;
      Tuple<int, int> parsedSeat = ParseSeat(Seat);
      
      bool isInRange = parsedSeat.Item1 <= numRow && parsedSeat.Item2 <= numCol;
      if(!isInRange) throw new Exception("Seat Invalid!");
    }

    public Ticket BookTicket(TicketBookDto dto, string UserId)
    {
      CheckSeatInRange(dto.Seat, dto.ShowTimeId);
      CheckDupplicatedSeat(dto.Seat, dto.ShowTimeId);

      Ticket entity = new Ticket{
        Seat = dto.Seat,
        ShowTimeId = dto.ShowTimeId,
        TicketTypeId = dto.TicketTypeId,
        UserId = UserId
      };
      return repository.Add(entity);
    }

    public Ticket CreateTicket(TicketCreateDto dto)
    {
      return BookTicket(dto, dto.UserId);
    }

  }
}