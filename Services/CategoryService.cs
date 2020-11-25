using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
namespace Cinemo.Service
{
  public class CategoryService{
    private CategoryRepository repository;
    public CategoryService(CategoryRepository CategoryRepository){
      this.repository = CategoryRepository;
    }

    public List<Category> GetAll(){
      return repository.FindAll();
    }

    public Category GetDetail(int id){
      return repository.FindById(id);
    }

    public Category GetDetail(string name){
      name = FormatString.Trim_MultiSpaces_Title(name);
      return repository.FindAll().Where(c => c.Name.Equals(name)).FirstOrDefault();
    }

    public Category Delete(int id){
      return repository.Delete(id);
    }

    public Category Create(CategoryCreateDto dto){
      var entity = new Category {
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name)
      };

      return repository.Add(entity);
    }

    public Category Update(CategoryUpdateDto dto){
      var entity = new Category {
        Id = dto.Id,
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name)
      };
      return repository.Update(entity);
    }
  }
}