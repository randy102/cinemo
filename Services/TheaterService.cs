using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Service
{
  public class TheaterService{
    private TheaterRepository repository;
    private ShowTimeRepository showTimeRepository;

    public TheaterService(TheaterRepository TheaterRepository, ShowTimeRepository showTimeRepository) {
      this.repository = TheaterRepository;
      this.showTimeRepository = showTimeRepository;
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
    
    public List<SelectListItem> GetSelectListItems(int defaultId = 0)
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Name,
        Selected = c.Id == defaultId
      }).ToList();
    }

    public Theater Delete(int id) {
      bool isShowTimeCreated = showTimeRepository.FindWhere(s => s.TheaterId == id).Any();
      if(isShowTimeCreated) throw new Exception("Không thể xóa phòng chiếu đã tạo lịch");

      var  existed = GetDetail(id);
      if(existed.Rooms.Any()) throw new Exception("Can not delete Theater that already had Rooms");

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