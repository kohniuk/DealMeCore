using DealMeCore.BusinessLogic.Models;
using DealMeCore.DataAccess.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealMeCore.BusinessLogic.Services
{
    /// <summary>
    /// IDealService interface
    /// </summary>
    public interface IDealService
    {
        /// <summary>
        /// Creates the deal.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<DealDto> CreateDeal(CreateDealDto createModel);

        /// <summary>
        /// Updates the deal.
        /// </summary>
        /// <param name="dealId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<DealDto> UpdateDeal(Guid dealId, UpdateDealDto updateModel);

        /// <summary>
        /// Deletes the deal.
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        Task<bool> DeleteDeal(Guid dealId);

        /// <summary>
        /// Get all deals.
        /// </summary>
        /// <returns></returns>
        Task<IList<DealDto>> GetAllDeals();

        /// <summary>
        /// Get all active deals.
        /// </summary>
        /// <returns></returns>
        Task<IList<DealDto>> GetAllActivedDeals();

        /// <summary>
        /// Get deal by id.
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        Task<DealDto> GetDealById(Guid dealId);

        /// <summary>
        /// Get all deals.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PagedResult<DealDto>> GetAllDeals(BasePagingDto param);

        /// <summary>
        /// Get nearest deals.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        Task<PagedResult<DealDto>> GetNearestDeals(SearchDealsRequestDto request);
    }
}
