using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;
using System.Linq;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Service
{
  public class MovieService
  {
    private MovieRepository repository;
    private UserManager<User> manager;
    private FileService fileService;

    public MovieService(MovieRepository MovieRepository, FileService fileService)
    {
      this.repository = MovieRepository;
      this.fileService = fileService;
    }

    public List<Movie> GetAll()
    {
      return repository.FindAll();
    }

    public Movie GetDetail(int id)
    {
      return repository.FindById(id);
    }

    public List<SelectListItem> GetSelectListItems()
    {
      return GetAll().Select(c => new SelectListItem
      {
        Value = c.Id.ToString(),
        Text = c.Title
      }).ToList();
    }

    public Movie Delete(int id)
    {
      var existed = GetDetail(id);
      if (existed.Img != null)
        fileService.Remove(existed.Img);
      return repository.Delete(id);
    }

    public Movie Create(MovieCreateDto dto, User author)
    {
      string imageName = fileService.Save(dto.Upload);
      var entity = new Movie
      {
        Title = dto.Title,
        Content = dto.Content,
        Tags = dto.Tags != null ? string.Join(',', dto.Tags) : null,
        AuthorId = author.Id,
        Img = imageName,
        CategoryId = dto.CategoryId,
        Runtime = dto.Runtime,
        CreatedAt = System.DateTime.Now
      };

      return repository.Add(entity);
    }

    public Movie Update(MovieUpdateDto dto)
    {
      var entity = this.GetDetail(dto.Id);
      entity.Title = dto.Title;
      entity.Runtime = dto.Runtime;
      entity.Content = dto.Content;
      entity.Tags = dto.Tags != null ? string.Join(',', dto.Tags) : null;
      entity.CategoryId = dto.CategoryId;

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