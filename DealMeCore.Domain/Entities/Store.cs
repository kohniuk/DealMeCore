using System;
using GeoAPI.Geometries;

namespace DealMeCore.Domain.Entities
{
    /// <summary>
    /// Store.
    /// </summary>
    public class Store : BaseEntity
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
        public IPoint Location { get; set; }

        /// <summary>
        /// Determine is disabled store.
        /// </summary>
        public bool? IsDisabled { get; set; }

        /// <summary>
        /// Brand Id.
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// Brand
        /// </summary>
        public Brand Brand { get; set; }
    }
}
