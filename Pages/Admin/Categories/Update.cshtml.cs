using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cinemo.Utils;
using Cinemo.Data;
using Cinemo.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages {
  public class UpdateCategoryModel : PageModel {
    private ApplicationDbContext db;

    public UpdateCategoryModel(ApplicationDbContext db) => this.db = db;

    public string ErrorMessage { get; set; }

    [BindProperty(SupportsGet = true)]
    public int id { get; set; }

    [BindProperty]
    public String newName { get; set; }

    public Category category { get; set; }

    public IActionResult OnGet() {
      category = db.Categories.Find(id);

      if (category == null) {
        return Redirect("/Admin/Categories");
      } else {
        return Page();
      }
    }

    public IActionResult OnPost() {
      category = db.Categories.Find(id);
      //Định dạng chuỗi
      newName = FormatString.Trim_MultiSpaces_Title(newName);
      if (category.Name.Equals(newName)) {
        ErrorMessage = "Enter new name";
        return Page();
      }
      //Kiểm tra tên đã được sử dụng hay chưa
      bool isExist = (db.Categories.Where(c => c.Name.Equals(newName))).ToList().Any();
      //Input hợp lệ và tên thể loại chưa tồn tại
      if (ModelState.IsValid && !isExist) {
        //Thay đổi tên
        category.Name = newName;
        //Cập nhật thay đổi
        db.Update(category);
        //Lưu thay đổi 
        db.SaveChanges();
        //Chuyển hướng đến trang ListCategory
        return Redirect("/Admin/Categories");
      } else {
        //Tên thể loại đã tồn tại => tạo thất bại
        //Lời nhắn 
        ErrorMessage = newName + " existed";
        //Trả về trang hiện tại( trang cập nhật thể loại)
        return Page();
      }
    }
  }
}
