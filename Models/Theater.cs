using System.Collections.Generic;
using Cinemo.Interface;

namespace Cinemo.Models {
  public class Theater: IEntity {
    public int Id { get; set; }
    public string Name {get; set; }
    public string Address {get; set; }

    public virtual List<Room> Rooms {get; set;} = new List<Room>();
  }

  public class TheaterCreateDto {
    public string Name {get; set; }
    public string Address {get; set; }
  }

  public class TheaterUpdateDto: TheaterCreateDto {
    public int Id { get; set; }
  }
}