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
            CreateMap<BoardCreationDTO,Board > ();
            CreateMap<BoardUpdateDTO,Board > ();
         
        }
     
    }
}