namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// Base paging DTO
    /// </summary>
    public class BasePagingDto
    {
        /// <summary>
        /// Pagination (skip).
        /// </summary>
        public virtual int Skip { get; set; }

        /// <summary>
        /// Pagination (take).
        /// </summary>
        public virtual int Take { get; set; }
    }
}
