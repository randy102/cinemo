
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Service
{
  public class ShowTimeService
  {
    private ShowTimeRepository repository;
    private RoomService roomService;
    private MovieService movieService;
    private readonly ILogger<ShowTimeService> _logger;

    public ShowTimeService(ShowTimeRepository showTimeRepository, RoomService roomService, MovieService movieService, ILogger<ShowTimeService> logger)
    {
      this.repository = showTimeRepository;
      this.roomService = roomService;
      this.movieService = movieService;
      this._logger = logger;
    }

    public List<ShowTime> GetAll()
    {
      return repository.FindAll();
    }

    public ShowTime GetDetail(int id)
    {
      return repository.FindById(id);
    }

    public List<ShowTime> GetAll(int roomId)
    {
      return repository.FindAll().Where(r => r.RoomId == roomId).ToList();
    }

    public List<SelectListItem> GetSelectListItems(int defaultId = 0)
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Time.ToString(),
        Selected = defaultId == c.Id
      }).ToList();
    }

    private void checkSupportedFormat(ShowTimeCreateDto dto)
    {
      Room room = roomService.GetDetail(dto.RoomId);
      if (!room.Formats.Contains(dto.Format.ToString()))
      {
        throw new Exception("Not support the format " + dto.Format + ". Only " + room.Formats + ".");
      }
    }

    private bool isExist(ShowTimeCreateDto dto, bool type = true)
    {
      //Phòng showTime được xét cần sử dụng
      var room = roomService.GetDetail(dto.RoomId);
      var movie = movieService.GetDetail(dto.MovieId);

      //Kiểm thời gian bắt đầu/ kết thúc showTime
      //Những showTime sử dụng phòng đang xét
      var showTimes = GetAll(dto.RoomId);
      //Thời gian bắt đầu/ kết thúc của showTime được xét
      var start = DateTimeUtils.Parse(dto.Time);

      var end = start.AddMinutes(movie.Length);
      foreach (var st in showTimes)
      {
        var stStart = st.Time;
        var stEnd = start.AddMinutes(st.Movie.Length);
        // if (dto.GetType() == typeof(Cinemo.Models.ShowTimeUpdateDto)){
        if (!type)
        {
          //Thời gian bắt đầu nằm trong thời gian của st
          //Thời gian kết thúc nằm trong thời gian của st
          var obj = (Cinemo.Models.ShowTimeUpdateDto)dto;
          if (obj.Id != st.Id)
          {
            if ((start >= stEnd && start <= stStart) || (end <= stStart && end >= stEnd))
            {
              throw new Exception("This room is not available until " + stEnd + ".");
            }
          }
        }
        else
        {
          //Thời gian bắt đầu nằm trong thời gian của st
          //Thời gian kết thúc nằm trong thời gian của st
          if ((start >= stEnd && start <= stStart) || (end <= stStart && end >= stEnd))
          {
            throw new Exception("This room is not available until " + stEnd + ".");
          }
        }
      }
      return false;
    }

    public ShowTime Delete(int id)
    {
      return repository.Delete(id);
    }

    public ShowTime Create(ShowTimeCreateDto dto)
    {
      checkSupportedFormat(dto);
      isExist(dto);

      var entity = new ShowTime
      {
        RoomId = dto.RoomId,
        TheaterId = roomService.GetDetail(dto.RoomId).TheaterId,
        MovieId = dto.MovieId,
        ExtraPrice = dto.ExtraPrice,
        Status = dto.Status,
        Type = dto.Type,
        Format = dto.Format,
        Time = DateTimeUtils.Parse(dto.Time)
      };

      return repository.Add(entity);
    }

    public ShowTime Update(ShowTimeUpdateDto dto)
    {
      checkSupportedFormat(dto);
      isExist(dto, false);
      var entity = new ShowTime
      {
        Id = dto.Id,
        RoomId = dto.RoomId,
        TheaterId = roomService.GetDetail(dto.RoomId).TheaterId,
        MovieId = dto.MovieId,
        ExtraPrice = dto.ExtraPrice,
        Status = dto.Status,
        Type = dto.Type,
        Format = dto.Format,
        Time = DateTimeUtils.Parse(dto.Time)
      };
      return repository.Update(entity);
    }

    public ShowTime ChangeStatus(ShowTime dto)
    {
      if (dto.Status == Cinemo.Models.ShowTime.ShowState.DRAFT || dto.Status == Cinemo.Models.ShowTime.ShowState.PUBLISH)
      {

        dto.Status = ((Cinemo.Models.ShowTime.ShowState)(dto.Status + 1));
      }
      else
      {
        dto.Status = ((Cinemo.Models.ShowTime.ShowState)(dto.Status - 1));
      }
      // var entity = new ShowTime
      // {
      //     Id = dto.Id,
      //     RoomId =dto.RoomId,//dto.Room.TheaterId,
      //     TheaterId =roomService.GetDetail(dto.RoomId).TheaterId,
      //     // TheaterId =1,
      //     MovieId=dto.MovieId,
      //     ExtraPrice=dto.ExtraPrice,
      //     Status=dto.Status,
      //     Type=dto.Type,
      //     Format=dto.Format,
      //     Time=dto.Time
      // };
      return repository.Update(dto);
    }
  }
}