using AutoMapper;
using Emocare.Application.DTOs.Chat;
using Emocare.Application.DTOs.Task;
using Emocare.Application.DTOs.User;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Entities.Task;


namespace Emocare.Application.Mappings
{
    public class MapperProfile: Profile
    {
        public MapperProfile() 
        { 
            CreateMap<Users,UserRegisterDto>().ReverseMap();
            CreateMap<UserProfileDto,Users>().ReverseMap();    
            CreateMap<InsertTaskDto,WellnessTask>().ReverseMap();
            CreateMap<ChatMessage, ChatResponseDto>().ForMember(des => des.Role, opt => opt.MapFrom(x => x.Role.ToString()));
        }
    }
}
