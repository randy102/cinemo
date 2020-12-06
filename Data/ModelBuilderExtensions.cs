using Cinemo.Models;
using Microsoft.EntityFrameworkCore;
namespace Cinemo.Data
{
  public static class ModelBuilderExtensions
  {
    public static ModelBuilder Seed(this ModelBuilder modelBuilder)
    {
      modelBuilder.Entity<TicketType>().HasData(
        new TicketType
        {
          Id = 1,
          Name = TicketType.Type.CHILDREN,
          Price = 40000
        },
        new TicketType
        {
          Id = 2,
          Name = TicketType.Type.NORMAL,
          Price = 90000
        },
        new TicketType
        {
          Id = 3,
          Name = TicketType.Type.STUDENT,
          Price = 50000
        },
        new TicketType
        {
          Id = 4,
          Name = TicketType.Type.VIP,
          Price = 120000
        }
      );

      modelBuilder.Entity<Theater>().HasData(
        new Theater
        {
          Id = 1,
          Name = "Cinemo Q1",
          Address = "43/5 NTMK, P.Đa Kao, Q1"
        },
        new Theater
        {
          Id = 2,
          Name = "Cinemo Bình Thạnh",
          Address = "54B CMT8, Q.Bình Thạnh"
        }
      );

      modelBuilder.Entity<Room>().HasData(
        new Room
        {
          Id = 1,
          Name = "Phòng 1",
          Formats = "_2D",
          NumCol = 10,
          NumRow = 8,
          TheaterId = 1,
          Total = 80
        },
        new Room
        {
          Id = 2,
          Name = "Phòng 2",
          Formats = "_3D",
          NumCol = 10,
          NumRow = 8,
          TheaterId = 1,
          Total = 80
        },
        new Room
        {
          Id = 3,
          Name = "Phòng 3",
          Formats = "_IMAX",
          NumCol = 10,
          NumRow = 8,
          TheaterId = 1,
          Total = 80
        },
        new Room
        {
          Id = 4,
          Name = "Phòng 1",
          Formats = "_IMAX, _2D, _3D",
          NumCol = 10,
          NumRow = 8,
          TheaterId = 2,
          Total = 80
        }
      );

      modelBuilder.Entity<Category>().HasData(
        new Category
        {
          Id = 1,
          Name = "Hành động"
        },
        new Category
        {
          Id = 2,
          Name = "Tình cảm"
        },
        new Category
        {
          Id = 3,
          Name = "Trinh Thám"
        }
      );

      modelBuilder.Entity<Movie>().HasData(
        new Movie
        {
          Id = 1,
          CategoryId = 1,
          Content = "Introducing...",
          Img = "9e2b848c-cbb4-4287-b813-afb853ce4754.png",
          Length = 125,
          Title = "Tom & Jerry",
        }
      );
      return modelBuilder;
    }
  }
}