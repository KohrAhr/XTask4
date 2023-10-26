namespace Lib.Suppliers.Types
{
    /// <summary>
    ///     https://suppliers-test.dev-dch.com/api/v1/BestRentals/AvailableOffers
    /// </summary>
    public class BestRentalsIncommingEntry
    {
        [SupplierBase("SupplierOfferID")]
        public string uniqueId { get; set; } = string.Empty;

        [SupplierBase("RentCost")]
        public float rentalCost { get; set; }

        [SupplierBase("RentCurrency")]
        public string rentalCostCurrency { get; set; } = string.Empty;

        [SupplierBase("CarDesc")]
        public string vehicle { get; set; } = string.Empty;

        [SupplierBase("CarID")]
        public string sipp { get; set; } = string.Empty;

        [SupplierBase("CarImage")]
        public string imageLink { get; set; } = string.Empty;

        [SupplierBase("CarLogoImage")]
        public string logo { get; set; } = string.Empty;
    }
}
