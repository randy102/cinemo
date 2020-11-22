using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
using Microsoft.AspNetCore.Http;

namespace Cinemo.Models {
  public class Movie: IEntity {
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

  public class MovieCreateDto {
    public int CategoryId {get; set;}=1;
    public string Tags { get; set; }
    public string Title {get; set;}
    public IFormFile Upload {get; set;}
    public string Content {get; set;}
  }

  public class MovieUpdateDto: MovieCreateDto {
    public int Id { get; set; }
  }
}