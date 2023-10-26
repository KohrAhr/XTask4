using Lib.Suppliers.Types;
using System.Collections.ObjectModel;

namespace Lib.Suppliers.Tests.TestData
{
    public static class SouthRentailsEntries
    {
        public static SouthRentalsIncommingEntry item1;
        public static SouthRentalsIncommingEntry item2;

        public static ObservableCollection<SouthRentalsIncommingEntry> southRentalsIncommingEntries = new();

        static SouthRentailsEntries()
        {
            item1 = new SouthRentalsIncommingEntry
            {
                quoteNumber = CommonData.CONST_SupplierID_V1,
                price = CommonData.CONST_RentCost_V1,
                currency = CommonData.CONST_RentCurrency_V1,
                vehicleName = CommonData.CONST_CarDesc_V1,
                acrissCode = CommonData.CONST_CarID_V1,
                imageLink = CommonData.CONST_CarImage_V1,
                logoLink = CommonData.CONST_CarLogoImage_V1
            };

            item2 = new SouthRentalsIncommingEntry
            {
                quoteNumber = CommonData.CONST_SupplierID_V2,
                price = CommonData.CONST_RentCost_V2,
                currency = CommonData.CONST_RentCurrency_V2,
                vehicleName = CommonData.CONST_CarDesc_V2,
                acrissCode = CommonData.CONST_CarID_V2,
                imageLink = CommonData.CONST_CarImage_V2,
                logoLink = CommonData.CONST_CarLogoImage_V2
            };

            //
            southRentalsIncommingEntries.Add(item1);
            southRentalsIncommingEntries.Add(item2);
        }
    }
}
