using AutoMapper;
using PersonalExpenseTracker.Models.Domain;
using PersonalExpenseTracker.Models.DTO;

namespace PersonalExpenseTracker.Mapping
{


   
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Domain → DTO
            CreateMap<User, UserDto>()
                .ForMember(dest => dest.Expenses, opt => opt.MapFrom(src => src.Expenses));

            CreateMap<AddExpenseRequestDto, Expense>();
            CreateMap<Expense, ExpenseDto>()
                .ForMember(dest => dest.UserName, opt => opt.MapFrom(src => src.User.Name));
            // DTO → Domain
            CreateMap<CreateUserDto, User>();
            CreateMap<AddExpenseRequestDto, Expense>();

        }
    }
}
