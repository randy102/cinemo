using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Cinemo.Service
{
  public class CategoryService
  {
    private CategoryRepository repository;
    public CategoryService(CategoryRepository CategoryRepository)
    {
      this.repository = CategoryRepository;
    }

    public List<Category> GetAll()
    {
      return repository.FindAll();
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

    public Category GetDetail(int id)
    {
      return repository.FindById(id);
    }

    public Category GetDetail(string name)
    {
      name = FormatString.Trim_MultiSpaces_Title(name);
      return repository.FindAll().Where(c => c.Name.Equals(name)).FirstOrDefault();
    }

    public Category Delete(int id)
    {
      var category = GetDetail(id);

      if (category.Movies.Any())
        throw new Exception("Category has been used!");
        
      return repository.Delete(id);
    }

    public Category Create(CategoryCreateDto dto)
    {
      var isExist = GetDetail(dto.Name);
      if (isExist != null)
      {
        throw new Exception(dto.Name + " existed");
      }
      var entity = new Category
      {
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name)
      };

      return repository.Add(entity);
    }

    public Category Update(CategoryUpdateDto dto)
    {
      var isExist = GetDetail(dto.Name);
      if (isExist != null && dto.Id != isExist.Id)
      {
        throw new Exception(dto.Name + " existed");
      }

      var entity = new Category
      {
        Id = dto.Id,
        Name = FormatString.Trim_MultiSpaces_Title(dto.Name)
      };
      return repository.Update(entity);
    }
  }
}