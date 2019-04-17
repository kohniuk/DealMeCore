namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// The Brand DTO.
    /// </summary>
    public class BrandDto : BaseEntityDto
    {
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
        /// Total deals count.
        /// </summary>
        public int TotalCountDeals { get; set; }

        /// <summary>
        /// Total stores count.
        /// </summary>
        public int TotalCountStores { get; set; }
    }
}
