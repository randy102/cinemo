using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
using System.Collections.Generic;
namespace Cinemo.Models
{
  public class Room : IEntity
  {
    public int Id { get; set; }
    public int TheaterId { get; set; }
    public virtual Theater Theater { get; set; }
    public string Name { get; set; }
    public int NumCol { get; set; }
    public int NumRow { get; set; }
    public int Total { get; set; }
    public string Formats { get; set; }
    public enum Format
    {
      _2D,
      _3D,
      _IMAX
    }
  }
  public class RoomCreateDto
  {
    public int TheaterId { get; set; }
    public string Name { get; set; }
    public int NumCol { get; set; }
    public int NumRow { get; set; }
    public int Total { get; set; }
    public string Formats { get; set; }
  }

  public class RoomUpdateDto : RoomCreateDto
  {
    public int Id { get; set; }
  }
}