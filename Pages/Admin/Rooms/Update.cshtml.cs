using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Cinemo.Pages.Admin.Room
{
    public class UpdateModel : PageModel
    {
        private Cinemo.Data.ApplicationDbContext db;
        public UpdateModel(Cinemo.Data.ApplicationDbContext db) => this.db = db;
        public string errMsg { get; set; }
        [BindProperty(SupportsGet = true)]
        public int id { get; set; }
        [BindProperty]
        public string name { get; set; }
        [BindProperty]
        public int numCol { get; set; }
        [BindProperty]
        public int numRow { get; set; }
        [BindProperty]
        public string formats { get; set; }
        public Cinemo.Models.Room room { get; set; }
    public IActionResult OnGet() {
      room = db.Rooms.Find(id);
      if (room == null) {
        return Redirect("./");
      } else {
        return Page();
      }
    }
    public IActionResult OnPost() {
      room = db.Rooms.Find(id);
      //Định dạng chuỗi
      name = Cinemo.Utils.FormatString.Trim_MultiSpaces_Title(name);
      //Kiểm tra tên đã được sử dụng hay chưa
      bool isExist = (db.Rooms.Where(r => !r.Id.Equals(room.Id) && r.Name.Equals(name))).ToList().Any();
      //Input hợp lệ và tên thể loại chưa tồn tại
      if (!isExist) {
        //Thay đổi tên
        room.Name = name;
        room.NumCol = numCol;
        room.NumRow = numRow;
        room.Formats = formats;
        room.Total=numRow*numCol;

        //Cập nhật thay đổi
        db.Update(room);
        //Lưu thay đổi 
        db.SaveChanges();
        //Chuyển hướng đến trang Listroom
        return Redirect("./");
      } else {
        //Tên thể loại đã tồn tại => tạo thất bại
        //Lời nhắn 
        errMsg = name + " existed";
        //Trả về trang hiện tại( trang cập nhật thể loại)
        return Page();
      }
    }
    }
}
