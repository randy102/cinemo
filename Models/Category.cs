using System.Collections.Generic;
using Cinemo.Interface;
namespace Cinemo.Models {
  public class Category : IEntity{
    public int Id { get; set; }
    public string Name {get; set; }

    public virtual List<Movie> Movies {get; set;} = new List<Movie>();
  }
public class CategoryCreateDto {
    public string Name {get; set; }
  }

  public class CategoryUpdateDto: CategoryCreateDto {
    public int Id { get; set; }
  }
}