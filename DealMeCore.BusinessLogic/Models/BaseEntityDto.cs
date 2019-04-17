using System;

namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// BaseEntityDto.
    /// </summary>
    public abstract class BaseEntityDto
    {
        /// <summary>
        /// Resource identifier.
        /// </summary>
        public Guid Id { get; set; }
    }
}
