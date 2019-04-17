namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// The Store DTO.
    /// </summary>
    public class StoreDto : BaseEntityDto
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
        /// Brand name
        /// </summary>
        public string BrandName { get; set; }
    }
}
