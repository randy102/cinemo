using System.ComponentModel.DataAnnotations;
using System;
using Cinemo.Interface;
using Microsoft.AspNetCore.Http;
using System.Collections.Generic;

namespace Cinemo.Models {
  public class Movie: IEntity {
    public int Id { get; set; }

    public int Length { get; set; }
    public int CategoryId {get; set;}=1;
    public string Tags { get; set; }
    public string Title {get; set;}
    public string Img {get; set;}
    public string Content {get; set;}
    
    [DataType(DataType.DateTime)]
    public DateTime CreatedAt {get; set;}

    [DisplayFormat(DataFormatString = "{HH:mm - dd/MM/yyyy}")]
    public DateTime Released {get; set;}

    public virtual Category Category {get; set;}
    public virtual List<ShowTime> ShowTimes {get; set;}
  }

  public class MovieCreateDto {
    public int CategoryId {get; set;}=1;
    public int Length { get; set; }
    public string[] Tags { get; set; }
    public string Title {get; set;}
    public IFormFile Upload {get; set;}
    public string Content {get; set;}
    
    [Required(ErrorMessage="You must enter a Date")]
    public string Released { get; set; }
  }

  public class MovieUpdateDto: MovieCreateDto {
    public int Id { get; set; }
  }
}