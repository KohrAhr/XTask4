using Lib.Suppliers.Types;
using System.Collections.ObjectModel;

namespace Lib.Suppliers.Tests.TestData
{
    public static class NorthernRentalsEntries
    {
        public static NorthernRentalsIncommingEntry item1;
        public static NorthernRentalsIncommingEntry item2;

        public static ObservableCollection<NorthernRentalsIncommingEntry> northernRentalsIncommingEntries = new();

        static NorthernRentalsEntries()
        {
            item1 = new NorthernRentalsIncommingEntry
            {
                id = CommonData.CONST_SupplierID_V1,
                price = CommonData.CONST_RentCost_V1,
                currency = CommonData.CONST_RentCurrency_V1,
                vehicleName = CommonData.CONST_CarDesc_V1,
                sippCode = CommonData.CONST_CarID_V1,
                image = CommonData.CONST_CarImage_V1,
                supplierLogo = CommonData.CONST_CarLogoImage_V1
            };

            item2 = new NorthernRentalsIncommingEntry
            {
                id = CommonData.CONST_SupplierID_V2,
                price = CommonData.CONST_RentCost_V2,
                currency = CommonData.CONST_RentCurrency_V2,
                vehicleName = CommonData.CONST_CarDesc_V2,
                sippCode = CommonData.CONST_CarID_V2,
                image = CommonData.CONST_CarImage_V2,
                supplierLogo = CommonData.CONST_CarLogoImage_V2
            };

            //
            northernRentalsIncommingEntries.Add(item1);
            northernRentalsIncommingEntries.Add(item2);
        }
    }
}
