using System.Collections.Generic;

namespace DealMeCore.Domain.Entities
{
    /// <summary>
    /// The Brand.
    /// </summary>
    public class Brand : BaseEntity
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Brand"/> class.
        /// </summary>
        public Brand()
        {
            this.Deals = new HashSet<Deal>();
            this.Stores = new HashSet<Store>();
        }

        /// <summary>
        /// Name of brand.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Description of brand.
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Determine is disabled brand.
        /// </summary>
        public bool? Disabled { get; set; }

        /// <summary>
        /// Image of brand.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Brand's URL.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Deals.
        /// </summary>
        public virtual ICollection<Deal> Deals { get; set; }

        /// <summary>
        /// Stores.
        /// </summary>
        public virtual ICollection<Store> Stores { get; set; }
    }
}
