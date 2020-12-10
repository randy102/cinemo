
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Service {
  public class ShowTimeService {
    private ShowTimeRepository repository;
    private MovieRepository movieRepository;
    private RoomService roomService;
    private DateTimeUtils dateTimeUtils;

    public ShowTimeService(MovieRepository movieRepository, ShowTimeRepository showTimeRepository, DateTimeUtils dateTimeUtils, RoomService roomService) {
      this.repository = showTimeRepository;
      this.roomService = roomService;
      this.movieRepository = movieRepository;
      this.dateTimeUtils = dateTimeUtils;
    }

    public List<ShowTime> GetAll() {
      return repository.FindAll();
    }

    public List<ShowTime> GetAllPublished() {
      return repository.FindWhere(s => s.Status == ShowTime.ShowState.PUBLISHED);
    }

    public ShowTime GetDetail(int id) {
      return repository.FindById(id);
    }

    public List<ShowTime> GetByRoomId(int roomId) {
      return repository.FindWhere(r => r.RoomId == roomId).ToList();
    }

    public List<ShowTime> GetByMovie(Movie movie) {
      return repository.FindWhere(r => r.MovieId == movie.Id).ToList();
    }

    public List<SelectListItem> GetShowingSelectListItems(int defaultId = 0) {
      return GetShowingTime().Select(c => new SelectListItem {
        Value = c.Id.ToString(),
        Text = c.Time.ToString(dateTimeUtils.FORMAT) + " - " + c.Movie.Title + " - " + c.Room.Name + " - " + c.Theater.Name,
        Selected = defaultId == c.Id
      }).ToList();
    }

    public FilmStateEnum GetFilmStatus(ShowTime showTime) {
      DateTime start = showTime.Time;
      DateTime end = showTime.Time.AddMinutes(showTime.Movie.Length);
      DateTime now = DateTime.Now;

      if (now < start) return FilmStateEnum.READY;
      if (end < now) return FilmStateEnum.END;
      return FilmStateEnum.PLAYING;
    }

    public bool isNotEnd(ShowTime showTime) {
      DateTime start = showTime.Time;
      DateTime end = start.AddMinutes(showTime.Movie.Length);
      DateTime now = DateTime.Now;
      return now <= end;
    }

    public List<ShowTime> GetShowingTime() {
      return GetAllPublished().Where(t => isNotEnd(t)).ToList();
    }

    public List<ShowTime> GetShowingTimesByMovieId(int movieId) {
      return GetShowingTime().Where(t => t.MovieId == movieId).ToList();
    }

    public List<ShowTime> GetShowTimesByFilters(int movieId, int theaterId, string date) {
      var showTimes = GetShowingTimesByMovieId(movieId);

      if (theaterId != 0)
        showTimes = showTimes.Where(s => s.TheaterId == theaterId).ToList();

      showTimes = showTimes.Where(s => {
        DateTime parsedDate = date != null
          ? DateTime.ParseExact(date, "dd-MM-yyyy", null).Date
          : DateTime.Today.Date;

        return s.Time.Date == parsedDate;
      }).ToList();

      return showTimes;
    }

    private void checkSupportedFormat(ShowTimeCreateDto dto) {
      Room room = roomService.GetDetail(dto.RoomId);
      if (!room.Formats.Contains(dto.Format.ToString())) {
        throw new Exception("Not support the format " + dto.Format + ". Only " + room.Formats + ".");
      }
    }


    private void checkDuplicated(ShowTimeCreateDto dto, int exceptId = 0) {
      Movie movie = movieRepository.FindById(dto.MovieId);
      List<ShowTime> showTimes = GetByRoomId(dto.RoomId);
      DateTime time = dateTimeUtils.Parse(dto.Time);

      ShowTime dupplicated = showTimes.Where(s => {
        var start = s.Time;
        var end = start.AddMinutes(s.Movie.Length);
        bool isBetween = start <= time && time <= end;
        return s.Id != exceptId && isBetween;
      }).FirstOrDefault();

      if (dupplicated != null) {
        DateTime dupEnd = dupplicated.Time.AddMinutes(dupplicated.Movie.Length);
        throw new Exception("Room is not available until " + dupEnd.ToString(dateTimeUtils.FORMAT));
      }
    }

    public ShowTime Delete(int id) {
      ShowTime showTime = GetDetail(id);
      CheckShowtimeHasTicket(showTime);
      return repository.Delete(id);
    }

    public ShowTime Create(ShowTimeCreateDto dto) {
      checkSupportedFormat(dto);
      checkDuplicated(dto);

      var entity = new ShowTime {
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

    public ShowTime Update(ShowTimeUpdateDto dto) {
      checkSupportedFormat(dto);
      checkDuplicated(dto, dto.Id);
      var entity = new ShowTime {
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

    public void CheckShowtimeHasTicket(ShowTime showTime) {
      if (showTime.Tickets.Any()) throw new Exception("Showtime has been booked!");
    }

    public ShowTime ChangeStatus(int id) {
      ShowTime showTime = GetDetail(id);
      CheckShowtimeHasTicket(showTime);

      if (showTime.Status == ShowTime.ShowState.DRAFT)
        showTime.Status = ShowTime.ShowState.PUBLISHED;
      else
        showTime.Status = ShowTime.ShowState.DRAFT;

      return repository.Update(showTime);
    }
  }
}