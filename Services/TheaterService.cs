using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;

namespace Cinemo.Service
{
  public class TheaterService{
    private TheaterRepository repository;
    public TheaterService(TheaterRepository theaterRepository){
      this.repository = theaterRepository;
    }

    public List<Theater> GetAll(){
      return repository.FindAll();
    }

    public Theater GetDetail(int id){
      return repository.FindById(id);
    }

    public Theater Delete(int id){
      return repository.Delete(id);
    }

    public Theater Create(TheaterCreateDto dto){
      var entity = new Theater {
        Name = dto.Name,
        Address = dto.Address
      };

      return repository.Add(entity);
    }

    public Theater Update(TheaterUpdateDto dto){
      var entity = new Theater {
        Id = dto.Id,
        Name = dto.Name,
        Address = dto.Address
      };
      return repository.Update(entity);
    }
  }
}