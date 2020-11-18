using System.ComponentModel.DataAnnotations;
using System;
namespace Cinemo.Models
{
    public class Room{
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
}