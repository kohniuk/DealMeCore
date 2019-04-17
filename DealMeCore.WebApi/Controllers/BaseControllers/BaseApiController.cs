using DealMeCore.BusinessLogic.Extensions;
using DealMeCore.BusinessLogic.Models;
using DealMeCore.Validation;
using Microsoft.AspNetCore.Mvc;

namespace DealMeCore.WebApi.Controllers.BaseControllers
{
    /// <summary>
    /// Base API controller.
    /// </summary>
    [Produces("application/json")]
    [Consumes("application/json")]
    [ApiController]
    public abstract class BaseApiController : Controller
    {
        private IValidationContext validationContext;

        /// <summary>
        /// The validation context.
        /// </summary>
        protected IValidationContext ValidationContext => this.validationContext
            ?? (this.validationContext = (IValidationContext)HttpContext.RequestServices.GetService(typeof(IValidationContext)));

        /// <summary>
        /// Generates Invalid request action result.
        /// </summary>
        /// <returns>Invalid request action result.</returns>
        protected ActionResult InvalidRequest()
        {
            if (ValidationContext.IsValid)
            {
                return NotFound();
            }

            return BadRequest(
                new ErrorResponseDto()
                {
                    Message = "Invalid request.",
                    Errors = validationContext.GetErrors().GenerateErrorMessageDetails()
                });
        }
    }
}