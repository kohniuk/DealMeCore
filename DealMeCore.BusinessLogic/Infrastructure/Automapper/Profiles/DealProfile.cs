using AutoMapper;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.BusinessLogic.Models.Enums;
using DealMeCore.Domain.Entities;

namespace DealMeCore.BusinessLogic.Infrastructure.Automapper.Profiles
{
    /// <summary>
    /// DealProfile.
    /// </summary>
    public class DealProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DealProfile"/> class.
        /// </summary>
        public DealProfile()
        {
            CreateMap<Deal, DealDto>()
                .ForMember(d => d.Gender, m => m.MapFrom(s => (GenderType)s.Gender))
                .ForMember(d => d.DealType, m => m.MapFrom(s => (DealType)s.DealType));

            CreateMap<CreateDealDto, Deal>()
                .ForMember(d => d.Gender, m => m.MapFrom(s => (byte)s.Gender))
                .ForMember(d => d.DealType, m => m.MapFrom(s => (byte)s.DealType));

            CreateMap<UpdateDealDto, Deal>()
                .ForMember(d => d.Gender, m => m.MapFrom(s => (byte)s.Gender))
                .ForMember(d => d.DealType, m => m.MapFrom(s => (byte)s.DealType));
        }
    }
}
