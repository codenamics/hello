using System.Collections.Generic;
using API.DTO;
using API.Entity;
using AutoMapper;
namespace API.Helpers
{
    public class AutoMapperProfiles : Profile
    {

        public AutoMapperProfiles()
        {
            CreateMap<Board, BoardDTO>()
            .ForMember(x => x.Lists, options => options.MapFrom(MapList));


        }
        private List<ListDTO> MapList(Board board, BoardDTO boardDTO)
        {
            var result = new List<ListDTO>();

            if (board.Lists != null)
            {
                foreach (var ls in board.Lists)
                {
                    result.Add(new ListDTO()
                    {
                        Id = ls.Id,
                        Title = ls.Title ,
                        Order = ls.Order
                    });
                }
            }

            return result;
        }
    }
}