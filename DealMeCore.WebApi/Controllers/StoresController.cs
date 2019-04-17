using DealMeCore.BusinessLogic.Models;
using DealMeCore.BusinessLogic.Services;
using DealMeCore.DataAccess.DB;
using DealMeCore.WebApi.Controllers.BaseControllers;
using DealMeCore.WebApi.Models;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Controllers
{
    /// <summary>
    /// StroesController. 
    /// </summary>
    [Route("api/stores")]
    [ApiController]
    public class StoresController : BaseApiController
    {
        private readonly IStoreService storeService;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoresController" /> class.
        /// </summary>
        public StoresController(IStoreService storeService)
        {
            this.storeService = storeService;
        }

        /// <summary>
        /// Create store.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Store has been created.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation("Create store.")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(StoreDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<StoreDto>> CreateStore([FromBody] CreateStoreRequestModel requestModel)
        {
            StoreDto responseModel = await storeService.CreateStore(
                new CreateStoreDto
                {
                    Name = requestModel.Name,
                    Description = requestModel.Description,
                    BrandId = requestModel.BrandId,
                    Address = requestModel.Address,
                    Location = new GeoEntry { Longitude = requestModel.Longitude, Latitude = requestModel.Latitude }
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return CreatedAtAction(nameof(CreateStore), new { id = responseModel.Id }, responseModel);
        }

        /// <summary>
        /// Update store.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Store has been updated.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Store doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{storeId:guid}")]
        [SwaggerOperation("Update store.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(StoreDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<StoreDto>> UpdateStore(
            Guid storeId,
            [FromBody] UpdateStoreRequestModel requestModel)
        {
            StoreDto responseModel = await storeService.UpdateStore(
                storeId,
                new UpdateStoreDto()
                {
                    Name = requestModel.Name,
                    Description = requestModel.Description,
                    BrandId = requestModel.BrandId,
                    Address = requestModel.Address,
                    IsDisabled = requestModel.IsDisabled,
                    Location = new GeoEntry { Longitude = requestModel.Longitude, Latitude = requestModel.Latitude }
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Delete store by id.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Store has been deleted.</response>
        /// <response code="404">Store doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{storeId:guid}")]
        [SwaggerOperation("Deletes store by id.")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<IActionResult> DeleteStore(Guid storeId)
        {
            bool result = await storeService.DeleteStore(storeId);

            if (!result)
            {
                return InvalidRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Get all stores.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Response with list of stores.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all stores.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedResult<StoreDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<PagedResult<StoreDto>>> GetStores([FromQuery]BasePagingDto request)
        {
            PagedResult<StoreDto> stores = await storeService.GetAllStores(request);

            if (stores == null)
            {
                return InvalidRequest();
            }

            return Ok(stores);
        }

        /// <summary>
        /// Get store by identifier.
        /// </summary>
        /// <param name="storeId"></param>
        /// <returns></returns>
        /// <response code="200">Response with store.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Store doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{storeId:guid}")]
        [SwaggerOperation("Get store by id.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(StoreDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<StoreDto>> GetStore(Guid storeId)
        {
            StoreDto store = await storeService.GetStore(storeId);

            if (store == null)
            {
                return InvalidRequest();
            }

            return Ok(store);
        }
    }
}