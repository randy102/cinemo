
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
namespace Cinemo.Service
{
    public class MovieService
    {
        private MovieRepository repository;
        public MovieService(MovieRepository movieRepository)
        {
            this.repository = movieRepository;
        }

        public List<Movie> GetAll()
        {
            return repository.FindAll();
        }

        public Movie GetDetail(int id)
        {
            return repository.FindById(id);
        }

        public List<Movie> GetAll(Category category)
        {
            var postsOfCate = repository.FindAll().Where(post => post.CategoryId == category.Id).ToList();
            return postsOfCate;
        }
    }
}