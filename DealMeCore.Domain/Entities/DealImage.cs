using System;

namespace DealMeCore.Domain.Entities
{
    /// <summary>
    /// The Deal image
    /// </summary>
    public class DealImage : BaseEntity
    {
        /// <summary>
        /// Path deal image.
        /// </summary>
        public string Path { get; set; }

        /// <summary>
        /// Deal id
        /// </summary>
        public Guid DealId { get; set; }

        /// <summary>
        /// Deal
        /// </summary>
        public virtual Deal Deal { get; set; }
    }
}
