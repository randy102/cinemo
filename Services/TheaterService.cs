using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
namespace Cinemo.Service
{
  public class TheaterService{
    private TheaterRepository repository;
    public TheaterService(TheaterRepository TheaterRepository) {
      this.repository = TheaterRepository;
    }

    public List<Theater> GetAll() {
      return repository.FindAll();
    }

    public Theater GetDetail(int id) {
      return repository.FindById(id);
    }

    public Theater GetDetail(string name) {
      name = FormatString.Trim_MultiSpaces_Title(name);
      return repository.FindAll().Where(c => c.Name.Equals(name)).FirstOrDefault();
    }

    public Theater Delete(int id) {
      return repository.Delete(id);
    }

    public Theater Create(TheaterCreateDto dto) {
      var isExist = GetDetail(dto.Name);
      if (isExist !=null) {
        throw new Exception(dto.Name+" existed");
      }
      var entity = new Theater {
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name),
        Address = FormatString.Trim_MultiSpaces_Title(dto.Address)
      };

      return repository.Add(entity);
    }

    public Theater Update(TheaterUpdateDto dto){
      var isExist = GetDetail(dto.Name);
      if (isExist !=null && dto.Id!=isExist.Id) {
        throw new Exception(dto.Name+" existed");
      }
      
      var entity = new Theater {
        Id = dto.Id,
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name),
        Address = FormatString.Trim_MultiSpaces_Title(dto.Address)
      };
      return repository.Update(entity);
    }
  }
}