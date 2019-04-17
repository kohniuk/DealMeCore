using System;

namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// CreateStoreDto.
    /// </summary>
    public class CreateStoreDto
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
        /// Store location.
        /// </summary>
        public GeoEntry Location { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public Guid BrandId { get; set; }
    }
}
