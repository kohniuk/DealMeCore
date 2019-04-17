using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace DealMeCore.WebApi.Models
{
    /// <summary>
    /// CreateBrandRequestModel.
    /// </summary>
    public class CreateBrandRequestModel
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
        /// Image of brand.
        /// </summary>
        [Required]
        public IFormFile Image { get; set; }

        /// <summary>
        /// Brand's URL.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Url { get; set; }
    }
}
