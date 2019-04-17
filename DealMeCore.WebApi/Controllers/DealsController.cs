using DealMeCore.BusinessLogic.Models;
using DealMeCore.BusinessLogic.Services;
using DealMeCore.DataAccess.DB;
using DealMeCore.WebApi.Controllers.BaseControllers;
using DealMeCore.WebApi.Models;
using DealMeCore.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Controllers
{
    /// <summary>
    /// DealsController.
    /// </summary>
    [Route("api/deals")]
    [ApiController]
    public class DealsController : BaseApiController
    {
        private readonly IDealService dealService;
        private readonly IImageWriter imageWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="DealsController" /> class.
        /// </summary>
        public DealsController(IDealService dealService, IImageWriter imageWriter)
        {
            this.dealService = dealService;
            this.imageWriter = imageWriter;
        }

        /// <summary>
        /// Create deal.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Deal has been created.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation("Create deal.")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(DealDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<DealDto>> CreateDeal([FromBody] CreateDealRequestModel requestModel)
        {
            DealDto responseModel = await dealService.CreateDeal(
                new CreateDealDto
                {
                    Name = requestModel.Name,
                    Currency = requestModel.Currency,
                    OriginalPrice = requestModel.OriginalPrice,
                    Price = requestModel.Price,
                    DealValidFrom = requestModel.DealValidFrom,
                    DealValidTo = requestModel.DealValidTo,
                    MarketUrl = requestModel.MarketUrl,
                    BuyNowUrl = requestModel.BuyNowUrl,
                    Gender = requestModel.Gender,
                    DealType = requestModel.DealType,
                    BrandId = requestModel.BrandId,
                    CouponCode = requestModel.CouponCode,
                    Images = requestModel.Images.Select(i => imageWriter.UploadImage(i).Result)
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return CreatedAtAction(nameof(CreateDeal), new { id = responseModel.Id }, responseModel);
        }

        /// <summary>
        /// Update deal.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Deal has been updated.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Deal doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{dealId:guid}")]
        [SwaggerOperation("Update deal.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DealDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<DealDto>> UpdateDeal(
            Guid dealId,
            [FromBody] UpdateDealRequestModel requestModel)
        {
            DealDto responseModel = await dealService.UpdateDeal(
                dealId,
                new UpdateDealDto
                {
                    Name = requestModel.Name,
                    Currency = requestModel.Currency,
                    OriginalPrice = requestModel.OriginalPrice,
                    Price = requestModel.Price,
                    DealValidFrom = requestModel.DealValidFrom,
                    DealValidTo = requestModel.DealValidTo,
                    MarketUrl = requestModel.MarketUrl,
                    BuyNowUrl = requestModel.BuyNowUrl,
                    Gender = requestModel.Gender,
                    DealType = requestModel.DealType,
                    BrandId = requestModel.BrandId,
                    IsActive = requestModel.IsActive,
                    CouponCode = requestModel.CouponCode,
                    Images = requestModel.Images.Select(i => imageWriter.UploadImage(i).Result)
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Delete deal by id.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Deal has been deleted.</response>
        /// <response code="404">Deal doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{dealId:guid}")]
        [SwaggerOperation("Deletes deal by id.")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound)]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<IActionResult> DeleteDeal(Guid dealId)
        {
            bool result = await dealService.DeleteDeal(dealId);

            if (!result)
            {
                return InvalidRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Get all pagination deals.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Response with list of deals.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all pagination deals.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedResult<DealDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<PagedResult<DealDto>>> GetDealsPagination([FromQuery]BasePagingDto request)
        {
            PagedResult<DealDto> deals = await dealService.GetAllDeals(request);

            if (deals == null)
            {
                return InvalidRequest();
            }

            return Ok(deals);
        }

        /// <summary>
        /// Get all nearest deals.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Response with list of deals.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("NearestDeals")]
        [SwaggerOperation("Get all nearest deals.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedResult<DealDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<PagedResult<DealDto>>> GetNearestDeals([FromQuery]SearchDealsRequestDto request)
        {
            PagedResult<DealDto> deals = await dealService.GetNearestDeals(request);

            if (deals == null)
            {
                return InvalidRequest();
            }

            return Ok(deals);
        }

        /// <summary>
        /// Get deals by identifier.
        /// </summary>
        /// <param name="dealId"></param>
        /// <returns></returns>
        /// <response code="200">Response with deal.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Deal doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{dealId:guid}")]
        [SwaggerOperation("Get deal by id.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(DealDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<DealDto>> GetDeal(Guid dealId)
        {
            DealDto deal = await dealService.GetDealById(dealId);

            if (deal == null)
            {
                return InvalidRequest();
            }

            return Ok(deal);
        }
    }
}
