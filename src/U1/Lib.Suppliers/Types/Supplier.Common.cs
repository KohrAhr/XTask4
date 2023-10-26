using System.ComponentModel.DataAnnotations;

namespace Lib.Suppliers.Types
{
    /// <summary>
    ///     Ok, here is my general type, 
    ///     for all suppliers. DB Ready
    /// </summary>
    public class SupplierCommon
    {
        /// <summary>
        ///     0 is Unknown
        /// </summary>
        // Yep, int. otherwise I have problem with type covertion in CoreDbHelper
        public int SupplierID { get; set; } = 0;

        /// <summary>
        ///     Not null
        /// </summary>
        [MaxLength(25)]
        public string SupplierOfferID { get; set; } = string.Empty;
    
        /// <summary>
        ///     Not null
        /// </summary>
        public float RentCost { get; set; }

        [MaxLength(5)]
        public string RentCurrency { get; set; } = string.Empty;

        [MaxLength(100)]
        public string CarDesc { get; set; } = string.Empty;

        /// <summary>
        ///     Not null
        /// </summary>
        [MaxLength(25)]
        public string CarID { get; set; } = string.Empty;

        [MaxLength(250)]
        public string CarLogoImage { get; set; } = string.Empty;

        [MaxLength(250)]
        public string CarImage { get; set; } = string.Empty;

        /// <summary>
        ///     Date when entry has been added
        /// </summary>
        public DateTime EntryDateTime { get; set; } = DateTime.UtcNow;
    }
}
