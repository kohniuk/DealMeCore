namespace DealMeCore.BusinessLogic.Models
{
    public class CreateBrandDto
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
        /// Image of brand.
        /// </summary>
        public string Image { get; set; }

        /// <summary>
        /// Brand's URL.
        /// </summary>
        public string Url { get; set; }
    }
}
