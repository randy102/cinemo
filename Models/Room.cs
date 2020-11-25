using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
namespace Cinemo.Models
{
    public class Room : IEntity{
        public int Id { get; set; }
        public string Name { get; set; }
        public int NumCol { get; set; }
        public int NumRow { get; set; }
        public int Total { get; set; }
        public string Formats { get; set; }
        public enum Format {
            _2D, 
            _3D,
            _IMAX
        }
    }
public class RoomCreateDto {
    public string Name { get; set; }
        public int NumCol { get; set; }
        public int NumRow { get; set; }
        public int Total { get; set; }
        public string Formats { get; set; }
  }

  public class RoomUpdateDto: RoomCreateDto {
    public int Id { get; set; }
  }
}