using AutoMapper;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.Domain.Entities;
using GeoAPI.Geometries;
using NetTopologySuite;

namespace DealMeCore.BusinessLogic.Infrastructure.Automapper.Profiles
{
    /// <summary>
    /// StoreProfile.
    /// </summary>
    public class StoreProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="StoreProfile"/> class.
        /// </summary>
        public StoreProfile()
        {
            CreateMap<Store, StoreDto>();

            CreateMap<CreateStoreDto, Store>()
                .ForMember(d => d.Location,
                    m => m.MapFrom(s =>
                        NtsGeometryServices.Instance.CreateGeometryFactory(4326)
                            .CreatePoint(new Coordinate(s.Location.Longitude, s.Location.Longitude))));

            CreateMap<UpdateStoreDto, Store>()
                .ForMember(d => d.Location,
                    m => m.MapFrom(s =>
                        NtsGeometryServices.Instance.CreateGeometryFactory(4326)
                            .CreatePoint(new Coordinate(s.Location.Longitude, s.Location.Longitude))));
        }
    }
}
