using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
using System.ComponentModel.DataAnnotations.Schema;
using System.Collections.Generic;

namespace Cinemo.Models
{
  public class ShowTime : IEntity
  {
    public int Id { get; set; }
    public int TheaterId { get; set; }
    public int RoomId { get; set; }
    public int MovieId { get; set; }
    public float ExtraPrice { get; set; }

    public ShowState Status { get; set; }
    public ShowType Type { get; set; }
    public FormatEnum Format { get; set; }

    [DisplayFormat(DataFormatString = "{HH:mm - dd/MM/yyyy}")]
    public DateTime Time { get; set; }

    public virtual Room Room { get; set; }
    public virtual Theater Theater { get; set; }
    public virtual Movie Movie { get; set; }
    public virtual List<Ticket> Tickets {get; set; }


    public enum ShowState
    {
      DRAFT,
      PUBLISHED
    }
    public enum ShowType
    {
      SUB,
      DUB,
      VN
    }
    public enum FormatEnum
    {
      _2D,
      _3D,
      _IMAX
    }
  }

  public enum FilmStateEnum
  {
    READY,
    PLAYING,
    END
  }
  public class ShowTimeCreateDto
  {
    public int TheaterId { get; set; }
    public int RoomId { get; set; }
    public int MovieId { get; set; }
    public ShowTime.ShowState Status { get; set; }
    public float ExtraPrice { get; set; }
    public ShowTime.ShowType Type { get; set; }
    public string Time { get; set; }
    public ShowTime.FormatEnum Format { get; set; }
  }

  public class ShowTimeUpdateDto : ShowTimeCreateDto
  {
    public int Id { get; set; }
  }
}