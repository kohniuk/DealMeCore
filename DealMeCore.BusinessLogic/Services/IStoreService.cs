using DealMeCore.BusinessLogic.Models;
using DealMeCore.DataAccess.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealMeCore.BusinessLogic.Services
{
    /// <summary>
    /// IStoreSrevice
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Creates the store.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<StoreDto> CreateStore(CreateStoreDto createModel);

        /// <summary>
        /// Updates the store.
        /// </summary>
        /// <param name="storeId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<StoreDto> UpdateStore(Guid storeId, UpdateStoreDto updateModel);

        /// <summary>
        /// Deletes the store.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<bool> DeleteStore(Guid storeId);

        /// <summary>
        /// Get all stores.
        /// </summary>
        /// <returns></returns>
        Task<IList<StoreDto>> GetAllStores();

        /// <summary>
        /// Get all enabled stores.
        /// </summary>
        /// <returns></returns>
        Task<IList<StoreDto>> GetAllEnabledStores();

        /// <summary>
        /// Get store by Id.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        Task<StoreDto> GetStore(Guid storeId);

        /// <summary>
        /// Get list of stores by Brand id.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        Task<IList<StoreDto>> GetStoresByBrandId(Guid brandId);

        /// <summary>
        /// Get all stores
        /// </summary>
        /// <param name="pagingParams"></param>
        /// <returns></returns>
        Task<PagedResult<StoreDto>> GetAllStores(BasePagingDto pagingParams);
    }
}
