using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;
using System;

namespace Cinemo.Service
{
  public class MovieService
  {
    private MovieRepository repository;
    private UserManager<User> manager;
    public MovieService(MovieRepository MovieRepository)
    {
      this.repository = MovieRepository;
    }

    public List<Movie> GetAll()
    {
      return repository.FindAll();
    }

    public Movie GetDetail(int id)
    {
      return repository.FindById(id);
    }
    public Movie Delete(int id)
    {
      return repository.Delete(id);
    }

    public Movie Create(MovieCreateDto dto, User author)
    {
      var entity = new Movie
      {
        Title = dto.Title,
        Content = dto.Content,
        Tags =  dto.Tags != null ? string.Join(',', dto.Tags) : null,
        AuthorId = author.Id,
        CategoryId = dto.CategoryId,
        CreatedAt = System.DateTime.Now
      };

      return repository.Add(entity);
    }

    public Movie Update(MovieUpdateDto dto)
    {
      var entity = this.GetDetail(dto.Id);
      entity.Title = dto.Title;
      entity.Content = dto.Content;
      entity.Tags = dto.Tags != null ? string.Join(',', dto.Tags) : null;
      entity.CategoryId = dto.CategoryId;
      return repository.Update(entity);
    }
  }
}