using System.ComponentModel.DataAnnotations;
using System;

namespace Cinemo.Models {
  public class Movie {
    public int Id { get; set; }

    public string AuthorId {get; set;}
    public virtual User Author {get; set;}

    public int CategoryId {get; set;}=1;
    public virtual Category Category {get; set;}
    public string Tags { get; set; }

    public string Title {get; set;}
    public string Img {get; set;}
    public string Content {get; set;}
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt {get; set;}
  }
}