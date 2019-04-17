using DealMeCore.BusinessLogic.Models;
using DealMeCore.DataAccess.DB;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace DealMeCore.BusinessLogic.Services
{
    /// <summary>
    /// IBrandService.
    /// </summary>
    public interface IBrandService
    {
        /// <summary>
        /// Creates the brand.
        /// </summary>
        /// <param name="createModel"></param>
        /// <returns></returns>
        Task<BrandDto> CreateBrand(CreateBrandDto createModel);

        /// <summary>
        /// Updates the brand.
        /// </summary>
        /// <param name="brandId"></param>
        /// <param name="updateModel"></param>
        /// <returns></returns>
        Task<BrandDto> UpdateBrand(Guid brandId, UpdateBrandDto updateModel);

        /// <summary>
        /// Deletes the brand.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        Task<bool> DeleteBrand(Guid brandId);

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <returns></returns>
        Task<IList<BrandDto>> GetAllBrands();

        /// <summary>
        /// Get all enabled brands.
        /// </summary>
        /// <returns></returns>
        Task<IList<BrandDto>> GetAllEnabledBrands();

        /// <summary>
        /// Get brand by id.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        Task<BrandDto> GetBrand(Guid brandId);

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <param name="param"></param>
        /// <returns></returns>
        Task<PagedResult<BrandDto>> GetAllBrands(BasePagingDto param);
    }
}
