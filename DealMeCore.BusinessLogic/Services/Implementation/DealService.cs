using AutoMapper;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.BusinessLogic.Models.Enums;
using DealMeCore.DataAccess.DB;
using DealMeCore.DataAccess.DB.EF.Extensions;
using DealMeCore.Domain.Entities;
using GeoAPI.Geometries;
using Microsoft.EntityFrameworkCore;
using NetTopologySuite;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DealMeCore.Validation;

namespace DealMeCore.BusinessLogic.Services.Implementation
{
    /// <summary>
    /// Deal Servise.
    /// </summary>
    public class DealService : IDealService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidationContext validationContext;
        private readonly IGeometryFactory geometryFactory;
        /// <summary>
        /// Initializes a new instance of the <see cref="DealService" /> class.
        /// </summary>
        public DealService(IUnitOfWork unitOfWork, IMapper mapper, IValidationContext validationContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.geometryFactory = NtsGeometryServices.Instance.CreateGeometryFactory(srid: 4326);
            this.validationContext = validationContext;
        }

        /// <summary>
        /// Creates the deal.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        public async Task<DealDto> CreateDeal(CreateDealDto createModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var dealModel = mapper.Map<CreateDealDto, Deal>(createModel);
            dealModel.IsActive = true;

            if (createModel.Images.Any())
            {
                dealModel.Images = createModel.Images
                    .Select(e => new DealImage { Path = e, Id = Guid.NewGuid() })
                    .ToList();
            }

            unitOfWork.GetRepository<Deal>().Insert(dealModel);

            await unitOfWork.SaveAsync();

            return mapper.Map<Deal, DealDto>(dealModel);
        }

        /// <summary>
        /// Updates the deal.
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        public async Task<DealDto> UpdateDeal(Guid dealId, UpdateDealDto updateModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var existingDeal = await unitOfWork.GetRepository<Deal>()
                .GetByIdAsync(dealId);

            if (existingDeal == null)
            {
                return null;
            }

            if (updateModel.Images.Any())
            {
                existingDeal.Images = updateModel.Images
                    .Select(e => new DealImage { Path = e, Id = Guid.NewGuid() })
                    .ToList();
            }

            mapper.Map<UpdateDealDto, Deal>(updateModel, existingDeal);

            unitOfWork.GetRepository<Deal>().Update(existingDeal);

            await unitOfWork.SaveAsync();

            return mapper.Map<Deal, DealDto>(existingDeal);
        }

        /// <summary>
        /// Deletes the deal.
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteDeal(Guid dealId)
        {
            var existingDeal = await unitOfWork.GetRepository<Deal>()
                .GetByIdAsync(dealId);

            if (existingDeal == null)
            {
                return false;
            }

            unitOfWork.GetRepository<Deal>().Delete(existingDeal);

            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Get all deals.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DealDto>> GetAllDeals()
        {
            var deals = await unitOfWork.GetRepository<Deal>()
                .GetAll()
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();

            return mapper.Map<IList<Deal>, IList<DealDto>>(deals);
        }

        /// <summary>
        /// Get all active deals.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<DealDto>> GetAllActivedDeals()
        {
            var deals = await unitOfWork.GetRepository<Deal>()
                .SearchFor(e =>
                    (e.DealValidFrom <= DateTime.Now) && (DateTime.Now <= e.DealValidTo) &&
                    !(e.Brand.Disabled ?? false))
                .OrderBy(e => e.Name)
                .ToListAsync();

            return mapper.Map<IList<Deal>, IList<DealDto>>(deals);
        }

        /// <summary>
        /// Get deal by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<DealDto> GetDealById(Guid id)
        {
            var deal = await unitOfWork.GetRepository<Deal>()
                .GetByIdAsync(id);

            return deal != null ? mapper.Map<Deal, DealDto>(deal) : null;
        }

        /// <summary>
        /// Get all deals.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PagedResult<DealDto>> GetAllDeals(BasePagingDto param)
        {
            return await unitOfWork.GetRepository<Deal>()
                .SearchFor(e =>
                    (e.DealValidFrom <= DateTime.Now) && (DateTime.Now <= e.DealValidTo) &&
                    !(e.Brand.Disabled ?? false))
                .Include(e => e.Brand)
                .Include(e => e.Images)
                .OrderBy(e => e.Name)
                .Select(e => new DealDto()
                {
                    Name = e.Name,
                    DealType = (DealType)e.DealType,
                    Gender = (GenderType)e.Gender,
                    Currency = e.Currency,
                    DealValidFrom = e.DealValidFrom,
                    DealValidTo = e.DealValidTo,
                    IsActive = e.IsActive,
                    OriginalPrice = e.OriginalPrice,
                    Price = e.Price,
                    BuyNowUrl = e.BuyNowUrl,
                    MarketUrl = e.MarketUrl,
                    CouponCode = e.CouponCode,
                    Images = e.Images.Select(i => i.Path)
                })
                .GetPagedResultAsync(param?.Skip, param?.Take);
        }

        /// <summary>
        /// Get nearest deals.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        public async Task<PagedResult<DealDto>> GetNearestDeals(SearchDealsRequestDto request)
        {
            var currentLocation =
                geometryFactory.CreatePoint(new Coordinate(request.GeoEntry.Longitude, request.GeoEntry.Latitude));

            return await unitOfWork.GetRepository<Deal>()
                .SearchFor(e =>
                    e.Brand.Stores.Any(s => s.Location.Distance(currentLocation) <= request.GeoEntry.Radius) &&
                    (e.DealValidFrom <= DateTime.Now) && (DateTime.Now <= e.DealValidTo) &&
                    !(e.Brand.Disabled ?? false))
                .Include(e => e.Brand)
                .Include(e => e.Images)
                .OrderBy(e => e.Name)
                .Select(e => new DealDto()
                {
                    Name = e.Name,
                    DealType = (DealType)e.DealType,
                    Gender = (GenderType)e.Gender,
                    Currency = e.Currency,
                    DealValidFrom = e.DealValidFrom,
                    DealValidTo = e.DealValidTo,
                    IsActive = e.IsActive,
                    OriginalPrice = e.OriginalPrice,
                    Price = e.Price,
                    BuyNowUrl = e.BuyNowUrl,
                    MarketUrl = e.MarketUrl,
                    CouponCode = e.CouponCode,
                    Images = e.Images.Select(i => i.Path)
                })
                .GetPagedResultAsync(request.Skip, request.Take);
        }
    }
}
