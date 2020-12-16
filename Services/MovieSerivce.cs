using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;
using Cinemo.Utils;

namespace Cinemo.Service
{
  public class MovieService
  {
    private MovieRepository repository;
    private FileService fileService;
    private DateTimeUtils dateTimeUtils;
    private ShowTimeService showTimeService;

    public MovieService(ShowTimeService showTimeService, MovieRepository MovieRepository, FileService fileService,  DateTimeUtils dateTimeUtils)
    {
      this.repository = MovieRepository;
      this.fileService = fileService;
      this.dateTimeUtils = dateTimeUtils;
      this.showTimeService = showTimeService;
    }

    public List<Movie> GetAll()
    {
      return repository.FindAll();
    }

    public List<Movie> GetShowingMovies(){
      return repository.FindWhere(movie => {
        bool isShowing = showTimeService.GetShowingTimesByMovieId(movie.Id).Any();
        return isShowing;
      });
    }

    public List<Movie> GetUpcomingMovies(){
      return repository.FindWhere(movie => {
        bool isNotReleased = DateTime.Now < movie.Released;
        bool isNotShowing = !showTimeService.GetShowingTimesByMovieId(movie.Id).Any();
        return isNotReleased && isNotShowing;
      });
    }

    public Movie GetDetail(int id)
    {
      return repository.FindById(id);
    }

    public List<SelectListItem> GetSelectListItems(int defaultId = 0)
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Title,
        Selected = c.Id == defaultId
      }).ToList();
    }

    public Movie Delete(int id)
    {
      var existed = GetDetail(id);
      if(existed.ShowTimes.Any()) throw new Exception("Can not delete Movie that already had Showtimes");

      if (existed.Img != null)
        fileService.Remove(existed.Img);

      return repository.Delete(id);
    }

    public Movie Create(MovieCreateDto dto)
    {
      string imageName = fileService.Save(dto.Upload);
      var entity = new Movie
      {
        Title = dto.Title,
        Content = dto.Content,
        Tags = dto.Tags != null ? string.Join(',', dto.Tags) : null,
        Img = imageName,
        CategoryId = dto.CategoryId,
        Length = dto.Length,
        CreatedAt = System.DateTime.Now,
        Released = dateTimeUtils.Parse(dto.Released)
      };

      return repository.Add(entity);
    }

    public Movie Update(MovieUpdateDto dto)
    {
      var entity = this.GetDetail(dto.Id);
      entity.Title = dto.Title;
      entity.Length = dto.Length;
      entity.Content = dto.Content;
      entity.Tags = dto.Tags != null ? string.Join(',', dto.Tags) : null;
      entity.CategoryId = dto.CategoryId;
      entity.Released = dateTimeUtils.Parse(dto.Released);

      if (dto.Upload != null)
      {
        if (entity.Img != null)
          fileService.Remove(entity.Img);

        entity.Img = fileService.Save(dto.Upload);
      }

      return repository.Update(entity);
    }
  }
}