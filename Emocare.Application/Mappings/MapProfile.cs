using AutoMapper;
using Emocare.Application.DTOs.Chat;
using Emocare.Application.DTOs.Common;
using Emocare.Application.DTOs.Habits;
using Emocare.Application.DTOs.Reflection;
using Emocare.Application.DTOs.Task;
using Emocare.Application.DTOs.User;
using Emocare.Domain.Entities.Auth;
using Emocare.Domain.Entities.Chat;
using Emocare.Domain.Entities.Habits;
using Emocare.Domain.Entities.Journal;
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
            CreateMap<JournalEntry, DailyResponseDto>().ForMember(des => des.Mood, opt => opt.MapFrom(x => x.Mood.ToString()))
                                                       .ForMember(des => des.Reflection, opt => opt.MapFrom(x => x.AIReflection));
            CreateMap<UserPushSubscription, RequestSubscription>();
            CreateMap<Habit,AddHabit>().ReverseMap();
        }
    }
}
