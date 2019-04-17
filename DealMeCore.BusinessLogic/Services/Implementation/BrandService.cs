using AutoMapper;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.DataAccess.DB;
using DealMeCore.DataAccess.DB.EF.Extensions;
using DealMeCore.Domain.Entities;
using DealMeCore.Validation;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DealMeCore.BusinessLogic.Services.Implementation
{
    /// <summary>
    /// Brand service.
    /// </summary>
    public class BrandService : IBrandService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidationContext validationContext;

        /// <summary>
        ///  Initializes a new instance of the <see cref="BrandService" /> class.
        /// </summary>
        public BrandService(IUnitOfWork unitOfWork, IMapper mapper, IValidationContext validationContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validationContext = validationContext;
        }

        /// <summary>
        /// Creates the brand.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        public async Task<BrandDto> CreateBrand(CreateBrandDto createModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var brandModel = mapper.Map<CreateBrandDto, Brand>(createModel);
            brandModel.Disabled = false;

            unitOfWork.GetRepository<Brand>().Insert(brandModel);

            await unitOfWork.SaveAsync();

            return mapper.Map<Brand, BrandDto>(brandModel);
        }

        /// <summary>
        /// Updates the brand.
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        public async Task<BrandDto> UpdateBrand(Guid brandId, UpdateBrandDto updateModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var existingBrand = await unitOfWork.GetRepository<Brand>()
                .GetByIdAsync(brandId);

            if (existingBrand == null)
            {
                return null;
            }

            mapper.Map<UpdateBrandDto, Brand>(updateModel, existingBrand);

            unitOfWork.GetRepository<Brand>().Update(existingBrand);

            await unitOfWork.SaveAsync();

            return mapper.Map<Brand, BrandDto>(existingBrand);
        }

        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteBrand(Guid brandId)
        {
            var existingBrand = await unitOfWork.GetRepository<Brand>()
                .GetByIdAsync(brandId);

            if (existingBrand == null)
            {
                return false;
            }

            unitOfWork.GetRepository<Brand>().Delete(existingBrand);

            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BrandDto>> GetAllBrands()
        {
            var brands = await unitOfWork.GetRepository<Brand>()
                .GetAll()
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();

            return mapper.Map<IList<Brand>, IList<BrandDto>>(brands);
        }

        /// <summary>
        /// Get all enabled brands
        /// </summary>
        /// <returns></returns>
        public async Task<IList<BrandDto>> GetAllEnabledBrands()
        {
            var enabledBrands = await unitOfWork.GetRepository<Brand>()
                .SearchFor(e => !e.Disabled.HasValue || e.Disabled.HasValue && !e.Disabled.Value)
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();

            return mapper.Map<IList<Brand>, IList<BrandDto>>(enabledBrands);
        }

        /// <summary>
        /// Get brand by id
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        public async Task<BrandDto> GetBrand(Guid brandId)
        {
            var brand = await unitOfWork.GetRepository<Brand>()
                .GetByIdAsync(brandId);

            return brand != null ? mapper.Map<Brand, BrandDto>(brand) : null;
        }

        /// <summary>
        /// Get all brands
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        public async Task<PagedResult<BrandDto>> GetAllBrands(BasePagingDto param)
        {
            return await unitOfWork.GetRepository<Brand>()
                .SearchFor(e => !e.Disabled.HasValue || e.Disabled.HasValue && !e.Disabled.Value)
                .Include(e => e.Deals)
                .Include(e => e.Stores)
                .OrderBy(e => e.Name)
                .Select(e => new BrandDto
                {
                    Name = e.Name,
                    Description = e.Description,
                    Image = e.Image,
                    Disabled = false,
                    Url = e.Url,
                    TotalCountDeals = e.Deals.Count,
                    TotalCountStores = e.Stores.Count
                })
                .GetPagedResultAsync(param?.Skip, param?.Take);
        }
    }
}
