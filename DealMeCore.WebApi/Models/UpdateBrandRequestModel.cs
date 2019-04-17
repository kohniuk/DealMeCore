using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DealMeCore.WebApi.Models
{
    /// <summary>
    /// UpdateBrandRequestModel
    /// </summary>
    public class UpdateBrandRequestModel
    {
        /// <summary>
        /// Name of brand.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// Description of brand.
        /// </summary>
        [Required]
        [StringLength(2000)]
        public string Description { get; set; }

        /// <summary>
        /// Determine is disabled brand.
        /// </summary>
        public bool? Disabled { get; set; }

        /// <summary>
        /// Image of brand.
        /// </summary>
        public IFormFile Image { get; set; }

        /// <summary>
        /// Brand's URL.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Url { get; set; }
    }
}
