namespace Cinemo.Models
{
    public class TicketType
    {
        public int Id { get; set; }
        public Type Name { get; set; }
        public float Price { get; set; }
        public enum Type
        {
            NORMAL,
            CHILDREN,
            STUDENT,
            VIP
        }
    }
}