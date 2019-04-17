using System.Collections.Generic;

namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// Represents base error response.
    /// </summary>
    public class ErrorResponseDto
    {
        /// <summary>
        /// Message.
        /// </summary>
        public string Message { get; set; }

        /// <summary>
        /// Additional details.
        /// </summary>
        public IList<FieldErrorDetailDto> Errors { get; set; }
    }
}
