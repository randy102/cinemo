using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
using System.Collections.Generic;
namespace Cinemo.Models
{
  public class Ticket : IEntity
  {
    public int Id { get; set; }
    public int ShowTimeId { get; set; }
    public int TicketTypeId { get; set; }
    public string UserId { get; set; }
    public string Seat { get; set; }
    
    public virtual User User { get; set; }
    public virtual TicketType TicketType { get; set; }
    public virtual ShowTime ShowTime { get; set; }
  }



  public class TicketBookDto
  {
    public int ShowTimeId { get; set; }
    public int TicketTypeId { get; set; }
    public string Seat { get; set; }
  }

  public class TicketCreateDto : TicketBookDto
  {
    public string UserId { get; set; }
  }

  public class TicketUpdateDto : RoomCreateDto
  {
    public int Id { get; set; }
  }
}