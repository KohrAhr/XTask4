namespace Lib.Suppliers.Types
{
    /// <summary>
    ///     https://suppliers-test.dev-dch.com/api/v1/SouthRentals/Quotes
    /// </summary>
    public class SouthRentalsIncommingEntry
    {
        [SupplierBase("SupplierOfferID")]
        public string quoteNumber { get; set; } = string.Empty;

        [SupplierBase("RentCost")]
        public float price { get; set; }

        [SupplierBase("RentCurrency")]
        public string currency { get; set; } = string.Empty;

        [SupplierBase("CarDesc")]
        public string vehicleName { get; set; } = string.Empty;

        [SupplierBase("CarID")]
        public string acrissCode { get; set; } = string.Empty;

        [SupplierBase("CarImage")]
        public string imageLink { get; set; } = string.Empty;

        [SupplierBase("CarLogoImage")]
        public string logoLink { get; set; } = string.Empty;
    }
}
