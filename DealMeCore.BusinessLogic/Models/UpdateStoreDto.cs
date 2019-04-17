using System;
using System.ComponentModel.DataAnnotations;

namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// UpdateStoreDto.
    /// </summary>
    public class UpdateStoreDto
    {
        /// <summary>
        /// Strore name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Store description.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Store address.
        /// </summary>
        public string Address { get; set; }

        /// <summary>
        /// Determine is disabled store.
        /// </summary>
        public bool? IsDisabled { get; set; }

        /// <summary>
        /// Store location.
        /// </summary>
        public GeoEntry Location { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public Guid BrandId { get; set; }
    }
}
