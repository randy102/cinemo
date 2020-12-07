
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
    private DateTimeUtils dateTimeUtils;
    private readonly ILogger<ShowTimeService> _logger;

    public ShowTimeService(ShowTimeRepository showTimeRepository, DateTimeUtils dateTimeUtils, RoomService roomService, MovieService movieService, ILogger<ShowTimeService> logger)
    {
      this.repository = showTimeRepository;
      this.roomService = roomService;
      this.movieService = movieService;
      this.dateTimeUtils = dateTimeUtils;
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
      return repository.FindWhere(r => r.RoomId == roomId).ToList();
    }

    public List<ShowTime> GetAll(Movie movie)
    {
      return repository.FindWhere(r => r.MovieId == movie.Id).ToList();
    }

    public List<SelectListItem> GetShowingSelectListItems(int defaultId = 0)
    {
      return GetShowingTime().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Time.ToString(dateTimeUtils.FORMAT) + " - " + c.Movie.Title + " - " + c.Room.Name + " - " + c.Theater.Name,
        Selected = defaultId == c.Id
      }).ToList();
    }

    public FilmStateEnum GetFilmStatus(ShowTime showTime)
    {
      DateTime start = showTime.Time;
      DateTime end = showTime.Time.AddMinutes(showTime.Movie.Length);
      DateTime now = DateTime.Now;

      if (now < start) return FilmStateEnum.READY;
      if (end < now) return FilmStateEnum.END;
      return FilmStateEnum.PLAYING;
    }

    public bool isNotEnd(ShowTime showTime)
    {
      DateTime start = showTime.Time;
      DateTime end = start.AddMinutes(showTime.Movie.Length);
      DateTime now = DateTime.Now;
      return now <= end;
    }

    // public List<ShowTime> GetShowingTime()
    // {
    //   return repository.FindWhere(t => isNotEnd(t));
    // }

    public List<ShowTime> GetShowingTime()
    {
      return repository.FindWhere(t => isNotEnd(t) && t.Status== ShowTime.ShowState.PUBLISHED).GroupBy(t => t.Movie.Title)
           .Select(t => t.First()).ToList();
    }

    public List<Movie> GetNotShowingTime()
    {
      DateTime now = DateTime.Now;
      var showTimes=GetAll();
      var movies=movieService.GetAll();
      var notShowingTimes=new List<Movie>();
      foreach (var movie in movies)
      {
          if (GetAll(movie).Count==0 && movie.Released > now)
          {
              notShowingTimes.Add(movie);
          }
      }
      return notShowingTimes.Distinct().ToList();
    }

    private void checkSupportedFormat(ShowTimeCreateDto dto)
    {
      Room room = roomService.GetDetail(dto.RoomId);
      if (!room.Formats.Contains(dto.Format.ToString()))
      {
        throw new Exception("Not support the format " + dto.Format + ". Only " + room.Formats + ".");
      }
    }


    private void checkDuplicated(ShowTimeCreateDto dto, int exceptId = 0)
    {
      var movie = movieService.GetDetail(dto.MovieId);
      var showTimes = GetAll(dto.RoomId);

      var time = dateTimeUtils.Parse(dto.Time);

      var dupplicated = showTimes.Where(s => {
        var start = s.Time;
        var end = start.AddMinutes(s.Movie.Length);
        bool isBetween = start <= time && time <= end;
        return s.Id != exceptId && isBetween;
      }).FirstOrDefault();

      if(dupplicated != null) {
        var dupEnd =  dupplicated.Time.AddMinutes(dupplicated.Movie.Length);
        throw new Exception("Room is not available until " + dupEnd.ToString(dateTimeUtils.FORMAT));
      }
    }

    public ShowTime Delete(int id)
    {
      return repository.Delete(id);
    }

    public ShowTime Create(ShowTimeCreateDto dto)
    {
      checkSupportedFormat(dto);
      checkDuplicated(dto);

      var entity = new ShowTime
      {
        RoomId = dto.RoomId,
        TheaterId = roomService.GetDetail(dto.RoomId).TheaterId,
        MovieId = dto.MovieId,
        ExtraPrice = dto.ExtraPrice,
        Status = dto.Status,
        Type = dto.Type,
        Format = dto.Format,
        Time = dateTimeUtils.Parse(dto.Time)
      };

      return repository.Add(entity);
    }

    public ShowTime Update(ShowTimeUpdateDto dto)
    {
      checkSupportedFormat(dto);
      checkDuplicated(dto, dto.Id);
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
        Time = dateTimeUtils.Parse(dto.Time)
      };
      return repository.Update(entity);
    }

    public ShowTime ChangeStatus(int id)
    {
      ShowTime showTime = GetDetail(id);
      if (showTime.Status == ShowTime.ShowState.DRAFT)
        showTime.Status = ShowTime.ShowState.PUBLISHED;
      else //TODO: Check if ticket is booked
        showTime.Status = ShowTime.ShowState.DRAFT;

      return repository.Update(showTime);
    }
  }
}