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
    /// Store service.
    /// </summary>
    public class StoreService : IStoreService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper;
        private readonly IValidationContext validationContext;

        /// <summary>
        ///  Initializes a new instance of the <see cref="StoreService" /> class.
        /// </summary>
        public StoreService(IUnitOfWork unitOfWork, IMapper mapper, IValidationContext validationContext)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.validationContext = validationContext;
        }

        /// <summary>
        /// Creates the store.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        public async Task<StoreDto> CreateStore(CreateStoreDto createModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var storeModel = mapper.Map<CreateStoreDto, Store>(createModel);
            storeModel.IsDisabled = false;

            unitOfWork.GetRepository<Store>().Insert(storeModel);

            await unitOfWork.SaveAsync();

            return mapper.Map<Store, StoreDto>(storeModel);
        }

        /// <summary>
        /// Updates the store.
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        public async Task<StoreDto> UpdateStore(Guid storeId, UpdateStoreDto updateModel)
        {
            if (!validationContext.IsValid)
            {
                return null;
            }

            var existingStore = await unitOfWork.GetRepository<Store>()
                .GetByIdAsync(storeId);

            if (existingStore == null)
            {
                return null;
            }

            mapper.Map<UpdateStoreDto, Store>(updateModel, existingStore);

            unitOfWork.GetRepository<Store>().Update(existingStore);

            await unitOfWork.SaveAsync();

            return mapper.Map<Store, StoreDto>(existingStore);
        }


        /// <summary>
        /// Deletes the store.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        public async Task<bool> DeleteStore(Guid storeId)
        {
            var existingStore = await unitOfWork.GetRepository<Store>()
                .GetByIdAsync(storeId);

            if (existingStore == null)
            {
                return false;
            }

            unitOfWork.GetRepository<Store>().Delete(existingStore);

            await unitOfWork.SaveAsync();

            return true;
        }

        /// <summary>
        /// Get all stores.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StoreDto>> GetAllStores()
        {
            var stores = await unitOfWork.GetRepository<Store>()
                .GetAll()
                .AsNoTracking()
                .OrderBy(e => e.Name)
                .ToListAsync();

            return mapper.Map<IList<Store>, IList<StoreDto>>(stores);
        }

        /// <summary>
        /// Get all enabled stores.
        /// </summary>
        /// <returns></returns>
        public async Task<IList<StoreDto>> GetAllEnabledStores()
        {
            var stores = await unitOfWork.GetRepository<Store>()
                .SearchFor(e => !e.IsDisabled.HasValue || e.IsDisabled.HasValue && !e.IsDisabled.Value)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return mapper.Map<IList<Store>, IList<StoreDto>>(stores);
        }

        /// <summary>
        /// Get store by Id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<StoreDto> GetStore(Guid id)
        {
            var store = await unitOfWork.GetRepository<Store>()
                .GetByIdAsync(id);

            return store != null ? mapper.Map<Store, StoreDto>(store) : null;
        }

        /// <summary>
        /// Get list of stores by Brand id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<IList<StoreDto>> GetStoresByBrandId(Guid id)
        {
            var stores = await unitOfWork.GetRepository<Store>()
                .SearchFor(s => s.BrandId == id)
                .OrderBy(s => s.Name)
                .ToListAsync();

            return mapper.Map<IList<Store>, IList<StoreDto>>(stores);
        }

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        public async Task<PagedResult<StoreDto>> GetAllStores(BasePagingDto pagingParams)
        {
            return await unitOfWork.GetRepository<Store>()
                .SearchFor(e => !e.IsDisabled.HasValue || e.IsDisabled.HasValue && !e.IsDisabled.Value)
                .Include(e => e.Brand)
                .OrderBy(e => e.Name)
                .Select(e => new StoreDto()
                {
                    Name = e.Name,
                    Description = e.Description,
                    Address = e.Address,
                    BrandName = e.Brand.Name
                })
                .GetPagedResultAsync(pagingParams?.Skip, pagingParams?.Take);
        }
    }
}
