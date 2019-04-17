namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// SearchDealsRequestDto.
    /// </summary>
    public class SearchDealsRequestDto : BasePagingDto
    {
        /// <summary>
        /// GeoEntry
        /// </summary>
        public GeoEntry GeoEntry { get; set; }
    }
}
