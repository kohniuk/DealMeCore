using DealMeCore.BusinessLogic.Models.Enums;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace DealMeCore.WebApi.Models
{
    /// <summary>
    /// CreateDealRequestModel.
    /// </summary>
    public class CreateDealRequestModel
    {
        /// <summary>
        /// Brand Id.
        /// </summary>
        [Required]
        public Guid BrandId { get; set; }

        /// <summary>
        /// Name of Deal.
        /// </summary>
        [Required]
        [StringLength(250)]
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
        [Required]
        public DateTime DealValidFrom { get; set; }

        /// <summary>
        /// Deal valid to.
        /// </summary>
        [Required]
        public DateTime DealValidTo { get; set; }

        /// <summary>
        /// Market URL.
        /// </summary>
        [Required]
        [StringLength(250)]
        public string MarketUrl { get; set; }

        /// <summary>
        /// Buy now URL
        /// </summary>
        [Required]
        [StringLength(250)]
        public string BuyNowUrl { get; set; }

        /// <summary>
        /// Gender.
        /// </summary>
        public GenderType Gender { get; set; }

        /// <summary>
        /// Type of deal.
        /// </summary>
        [Required]
        public DealType DealType { get; set; }

        /// <summary>
        /// Coupon code
        /// </summary>
        public string CouponCode { get; set; }

        /// <summary>
        /// Deal images
        /// </summary>
        public IEnumerable<IFormFile> Images { get; set; }
    }
}
