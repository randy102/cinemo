
using Cinemo.Interface;
namespace Cinemo.Models {
  public class TicketType : IEntity {
    public int Id { get; set; }
    public Type Name { get; set; }
    public float Price { get; set; }
    public enum Type {
      NORMAL,
      CHILDREN,
      STUDENT,
      VIP
    }
  }
  public class TicketTypeCreateDto {
    public TicketType.Type Name { get; set; }
    public float Price { get; set; }
  }
  public class TicketTypeUpdateDto : TicketTypeCreateDto {
    public int Id { get; set; }
  }
}