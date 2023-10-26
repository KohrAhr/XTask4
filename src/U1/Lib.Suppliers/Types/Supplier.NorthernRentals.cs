namespace Lib.Suppliers.Types
{
    /// <summary>
    ///     https://suppliers-test.dev-dch.com/api/v1/NorthernRentals/GetRates
    /// </summary>
    public class NorthernRentalsIncommingEntry
    {
        [SupplierBase("SupplierOfferID")]
        public string id { get; set; } = string.Empty;

        [SupplierBase("RentCost")]
        public float price { get; set; }

        [SupplierBase("RentCurrency")]
        public string currency { get; set; } = string.Empty;

        [SupplierBase("CarDesc")]
        public string vehicleName { get; set; } = string.Empty;

        [SupplierBase("CarID")]
        public string sippCode { get; set; } = string.Empty;

        [SupplierBase("CarImage")]
        public string image { get; set; } = string.Empty;

        [SupplierBase("CarLogoImage")]
        public string supplierLogo { get; set; } = string.Empty;
    }
}
