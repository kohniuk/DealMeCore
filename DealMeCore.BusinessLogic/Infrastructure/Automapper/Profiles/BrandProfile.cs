using AutoMapper;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.Domain.Entities;

namespace DealMeCore.BusinessLogic.Infrastructure.Automapper.Profiles
{
    /// <summary>
    /// BrandProfile.
    /// </summary>
    public class BrandProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="BrandProfile"/> class.
        /// </summary>
        public BrandProfile()
        {
            CreateMap<Brand, BrandDto>()
                .ForMember(d => d.TotalCountDeals, m => m.MapFrom(s => s.Deals.Count))
                .ForMember(d => d.TotalCountStores, m => m.MapFrom(s => s.Stores.Count));

            CreateMap<CreateBrandDto, Brand>();

            CreateMap<UpdateBrandDto, Brand>();
        }
    }
}
