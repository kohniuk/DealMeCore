using DealMeCore.BusinessLogic.Models;
using DealMeCore.BusinessLogic.Services;
using DealMeCore.DataAccess.Cache;
using DealMeCore.DataAccess.DB;
using DealMeCore.WebApi.Controllers.BaseControllers;
using DealMeCore.WebApi.Models;
using DealMeCore.WebApi.Utils;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Net;
using System.Threading.Tasks;

namespace DealMeCore.WebApi.Controllers
{
    /// <summary>
    /// BrandsController.
    /// </summary>
    [Route("api/brands")]
    [ApiController]
    public class BrandsController : BaseApiController
    {
        private readonly IBrandService brandService;
        private readonly ICacheProvider cacheProvider;
        private readonly IImageWriter imageWriter;

        /// <summary>
        /// Initializes a new instance of the <see cref="BrandsController" /> class.
        /// </summary>
        public BrandsController(
            IBrandService brandService,
            ICacheProvider cacheProvider,
            IImageWriter imageWriter)
        {
            this.brandService = brandService;
            this.cacheProvider = cacheProvider;
            this.imageWriter = imageWriter;
        }

        /// <summary>
        /// Create brand.
        /// </summary>
        /// <returns></returns>
        /// <response code="201">Brand has been created.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPost]
        [SwaggerOperation("Create brand.")]
        [ProducesResponseType((int)HttpStatusCode.Created, Type = typeof(BrandDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<BrandDto>> CreateBrand([FromBody] CreateBrandRequestModel requestModel)
        {
            BrandDto responseModel = await brandService.CreateBrand(
                new CreateBrandDto
                {
                    Name = requestModel.Name,
                    Description = requestModel.Description,
                    Url = requestModel.Description,
                    Image = await imageWriter.UploadImage(requestModel.Image)
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return CreatedAtAction(nameof(CreateBrand), new { id = responseModel.Id }, responseModel);
        }

        /// <summary>
        /// Update brand.
        /// </summary>
        /// <returns></returns>
        /// <response code="200">Brand has been updated.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Brand doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpPut("{brandId:guid}")]
        [SwaggerOperation("Update brand.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BrandDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<BrandDto>> UpdateBrand(
            Guid brandId,
            [FromBody] UpdateBrandRequestModel requestModel)
        {
            BrandDto responseModel = await brandService.UpdateBrand(
                brandId,
                new UpdateBrandDto
                {
                    Name = requestModel.Name,
                    Description = requestModel.Description,
                    Url = requestModel.Description,
                    Image = await imageWriter.UploadImage(requestModel.Image)
                });

            if (responseModel == null)
            {
                return InvalidRequest();
            }

            return Ok(responseModel);
        }

        /// <summary>
        /// Delete brand by id.
        /// </summary>
        /// <returns></returns>
        /// <response code="204">Brand has been deleted.</response>
        /// <response code="404">Brand doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpDelete("{brandId:guid}")]
        [SwaggerOperation("Deletes brand by id.")]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<IActionResult> DeleteBrand(Guid brandId)
        {
            bool result = await brandService.DeleteBrand(brandId);

            if (!result)
            {
                return InvalidRequest();
            }

            return NoContent();
        }

        /// <summary>
        /// Get all brands.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Response with list of brands.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("")]
        [SwaggerOperation("Get all brands.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedResult<BrandDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<PagedResult<BrandDto>>> GetBrands([FromQuery]BasePagingDto request)
        {
            PagedResult<BrandDto> brands =
                await cacheProvider.GetAsync("brands", async () => await brandService.GetAllBrands(request));

            if (brands == null)
            {
                return InvalidRequest();
            }

            return Ok(brands);
        }

        /// <summary>
        /// Get all pagination brands.
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        /// <response code="200">Response with list of brands.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet]
        [Route("pagination")]
        [SwaggerOperation("Get all brands.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(PagedResult<BrandDto>))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<PagedResult<BrandDto>>> GetBrandsPagination([FromQuery]BasePagingDto request)
        {
            PagedResult<BrandDto> brands = await brandService.GetAllBrands(request);

            if (brands == null)
            {
                return InvalidRequest();
            }

            return Ok(brands);
        }

        /// <summary>
        /// Gets brand by identifier.
        /// </summary>
        /// <param name="brandId"></param>
        /// <returns></returns>
        /// <response code="200">Response with brand.</response>
        /// <response code="400">Invalid parameters.</response>
        /// <response code="404">Brand doesn't exist.</response>
        /// <response code="500">Internal server error.</response>
        [HttpGet("{brandId:guid}")]
        [SwaggerOperation("Gets brand by id.")]
        [ProducesResponseType((int)HttpStatusCode.OK, Type = typeof(BrandDto))]
        [ProducesResponseType((int)HttpStatusCode.BadRequest, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.NotFound, Type = typeof(ErrorResponseDto))]
        [ProducesResponseType((int)HttpStatusCode.InternalServerError, Type = typeof(ErrorResponseDto))]
        public async Task<ActionResult<BrandDto>> GetBrand(Guid brandId)
        {
            BrandDto brand = await brandService.GetBrand(brandId);

            if (brand == null)
            {
                return InvalidRequest();
            }

            return Ok(brand);
        }
    }
}