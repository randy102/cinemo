
using Cinemo.Repository;
using Cinemo.Models;
using System.Collections.Generic;
using Cinemo.Utils;
using System.Linq;
using System;
namespace Cinemo.Service
{
    public class ShowTimeService
    {
        private ShowTimeRepository repository;
        private RoomService roomService;
        private MovieService movieService;
        public ShowTimeService(ShowTimeRepository showTimeRepository,RoomService roomService,MovieService movieService)
        {
            this.repository = showTimeRepository;
            this.roomService=roomService;
            this.movieService=movieService;
        }

        public List<ShowTime> GetAll()
        {
            return repository.FindAll();
        }

        public ShowTime GetDetail(int id)
        {
            return repository.FindById(id);
        }
        // public Room GetDetail(int theaterId, string name)
        // {
        //     name = FormatString.Trim_MultiSpaces_Title(name);
        //     return repository.FindAll().Where(c => c.TheaterId == theaterId && c.Name.Equals(name)).FirstOrDefault();
        // }
        public List<ShowTime> GetAll(int roomId) {
            return repository.FindAll().Where(r => r.RoomId==roomId).ToList();
        }

        //true = create
        //fase=update
        public bool isExist(ShowTimeCreateDto dto,bool type=true) { 
            //Phòng showTime được xét cần sử dụng
            var room=roomService.GetDetail(dto.RoomId);
            var movie=movieService.GetDetail(dto.MovieId);

            //Kiểm tra format 
            string dtoFormat=Enum.GetName(typeof(Cinemo.Models.ShowTime.FormatEnum),(int) dto.Format);


            if(!room.Formats.Contains(dtoFormat)){
                throw new Exception("Not support the format "+dto.Format+". Only "+room.Formats+".");
            }

            //Kiểm thời gian bắt đầu/ kết thúc showTime
            //Những showTime sử dụng phòng đang xét
            var showTimes=GetAll(dto.RoomId);
            //Thời gian bắt đầu/ kết thúc của showTime được xét
            var start = dto.Time;
            var end=start.AddMinutes(movie.Runtime);
            foreach (var st in showTimes)   
            {
                var stStart = st.Time;
                    var stEnd=start.AddMinutes(st.Movie.Runtime);
                // if (dto.GetType() == typeof(Cinemo.Models.ShowTimeUpdateDto)){
                    if (!type){
                    //Thời gian bắt đầu nằm trong thời gian của st
                    //Thời gian kết thúc nằm trong thời gian của st
                    var obj=(Cinemo.Models.ShowTimeUpdateDto)dto;
                    if(obj.Id!=st.Id){
                        if((start>=stEnd&&start<=stStart)||(end<=stStart&&end>=stEnd)){
                            throw new Exception("This room is not available until "+ stEnd+".");
                        }
                    }
                }else{
                    //Thời gian bắt đầu nằm trong thời gian của st
                    //Thời gian kết thúc nằm trong thời gian của st
                    if((start>=stEnd&&start<=stStart)||(end<=stStart&&end>=stEnd)){
                        throw new Exception("This room is not available until "+ stEnd+".");
                    }
                }
            }
            return false;
        }

        public ShowTime Delete(int id)
        {
            return repository.Delete(id);
        }

        public ShowTime Create(ShowTimeCreateDto dto)
        {
            // var isExist = GetDetail(dto.TheaterId, dto.Name);
            // if (isExist != null && dto.TheaterId == isExist.TheaterId)
            // {
            //     throw new Exception(dto.Name + " existed");
            // }
            isExist(dto);
            var entity = new ShowTime
            {
                RoomId =dto.RoomId,//dto.Room.TheaterId,
                TheaterId =roomService.GetDetail(dto.RoomId).TheaterId,
                // TheaterId =1,
                MovieId=dto.MovieId,
                ExtraPrice=dto.ExtraPrice,
                Status=dto.Status,
                Type=dto.Type,
                Format=dto.Format,
                Time=dto.Time
            };

            return repository.Add(entity);
        }

        public ShowTime Update(ShowTimeUpdateDto dto)
        {
            isExist(dto,false);
            var entity = new ShowTime
            {
                Id = dto.Id,
                RoomId =dto.RoomId,
                TheaterId =roomService.GetDetail(dto.RoomId).TheaterId,
                MovieId=dto.MovieId,
                ExtraPrice=dto.ExtraPrice,
                Status=dto.Status,
                Type=dto.Type,
                Format=dto.Format,
                Time=dto.Time
            };
            return repository.Update(entity);
        }

        public ShowTime ChangeStatus(ShowTime dto)
        {
            if (dto.Status == Cinemo.Models.ShowTime.ShowState.DRAFT || dto.Status == Cinemo.Models.ShowTime.ShowState.PUBLISH){
                
                dto.Status = ((Cinemo.Models.ShowTime.ShowState)(dto.Status+1));
            }
            else{
                dto.Status = ((Cinemo.Models.ShowTime.ShowState)(dto.Status-1));
            }
            // var entity = new ShowTime
            // {
            //     Id = dto.Id,
            //     RoomId =dto.RoomId,//dto.Room.TheaterId,
            //     TheaterId =roomService.GetDetail(dto.RoomId).TheaterId,
            //     // TheaterId =1,
            //     MovieId=dto.MovieId,
            //     ExtraPrice=dto.ExtraPrice,
            //     Status=dto.Status,
            //     Type=dto.Type,
            //     Format=dto.Format,
            //     Time=dto.Time
            // };
            return repository.Update(dto);
        }
    }
}