using DealMeCore.BusinessLogic.Models.Enums;
using System;
using System.Collections.Generic;

namespace DealMeCore.BusinessLogic.Models
{
    /// <summary>
    /// UpdateDealDto.
    /// </summary>
    public class UpdateDealDto
    {
        /// <summary>
        /// Brand Id.
        /// </summary>
        public Guid BrandId { get; set; }

        /// <summary>
        /// Name of Deal.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Currency.
        /// </summary>
        public string Currency { get; set; }

        /// <summary>
        /// Original price of deal.
        /// </summary>
        public decimal OriginalPrice { get; set; }

        /// <summary>
        /// Current deal price.
        /// </summary>
        public decimal Price { get; set; }

        /// <summary>
        /// Deal valid from.
        /// </summary>
        public DateTime DealValidFrom { get; set; }

        /// <summary>
        /// Deal valid to.
        /// </summary>
        public DateTime DealValidTo { get; set; }

        /// <summary>
        /// Market URL.
        /// </summary>
        public string MarketUrl { get; set; }

        /// <summary>
        /// Buy now URL
        /// </summary>
        public string BuyNowUrl { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Type of deal.
        /// </summary>
        public DealType DealType { get; set; }

        /// <summary>
        /// Coupon code
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// Is active deal.
        /// </summary>
        public bool IsActive { get; set; }

        /// <summary>
        /// Deal images
        /// </summary>
        public IEnumerable<string> Images { get; set; }
    }
}
