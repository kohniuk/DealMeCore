using System;
using System.ComponentModel.DataAnnotations;

namespace DealMeCore.WebApi.Models
{
    /// <summary>
    /// CreateStoreRequestModel.
    /// </summary>
    public class CreateStoreRequestModel
    {
        /// <summary>
        /// Strore name.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string Name { get; set; }

        /// <summary>
        /// Store description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Store address.
        /// </summary>
        [Required]
        [StringLength(400)]
        public string Address { get; set; }

        /// <summary>
        /// Latitude.
        /// </summary>
        [Required]
        public float Latitude { get; set; }

        /// <summary>
        /// Longitude.
        /// </summary>
        [Required]
        public float Longitude { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public Guid BrandId { get; set; }
    }
}
