using NUnit.Framework;
using Lib.Suppliers.Functions;
using Lib.Suppliers.Tests.TestData;
using Lib.Suppliers.Types;

namespace Lib.Suppliers.Tests
{
    /// <summary>
    ///     SouthRentals -- is #3
    /// </summary>
    public class Tests_SouthRentals_SignleEntry
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidEntry1_SouthRentals()
        {
            SupplierCommon result = ConvertFunctions.FromSupplier<SouthRentalsIncommingEntry>(SouthRentailsEntries.item1);

            Assert.AreEqual(result.SupplierID, Types.Suppliers.spSouthRentals);
            Assert.AreEqual(result.SupplierOfferID, CommonData.CONST_SupplierID_V1);
            Assert.AreEqual(result.RentCost, CommonData.CONST_RentCost_V1);
            Assert.AreEqual(result.RentCurrency, CommonData.CONST_RentCurrency_V1);
            Assert.AreEqual(result.CarDesc, CommonData.CONST_CarDesc_V1);
            Assert.AreEqual(result.CarID, CommonData.CONST_CarID_V1);
            Assert.AreEqual(result.CarLogoImage, CommonData.CONST_CarLogoImage_V1);
            Assert.AreEqual(result.CarImage, CommonData.CONST_CarImage_V1);
        }

        [Test]
        public void ValidEntry2_SouthRentals()
        {
            SupplierCommon result = ConvertFunctions.FromSupplier<SouthRentalsIncommingEntry>(SouthRentailsEntries.item2);

            Assert.AreEqual(result.SupplierID, Types.Suppliers.spSouthRentals);
            Assert.AreEqual(result.SupplierOfferID, CommonData.CONST_SupplierID_V2);
            Assert.AreEqual(result.RentCost, CommonData.CONST_RentCost_V2);
            Assert.AreEqual(result.RentCurrency, CommonData.CONST_RentCurrency_V2);
            Assert.AreEqual(result.CarDesc, CommonData.CONST_CarDesc_V2);
            Assert.AreEqual(result.CarID, CommonData.CONST_CarID_V2);
            Assert.AreEqual(result.CarLogoImage, CommonData.CONST_CarLogoImage_V2);
            Assert.AreEqual(result.CarImage, CommonData.CONST_CarImage_V2);
        }

        [Test]
        public void ValidEntry_DefValue_SouthRentals()
        {
            SupplierCommon result = ConvertFunctions.FromSupplier<SouthRentalsIncommingEntry>(new SouthRentalsIncommingEntry());

            Assert.AreEqual(result.SupplierID, Types.Suppliers.spSouthRentals);
            Assert.AreEqual(result.SupplierOfferID, string.Empty);
            Assert.AreEqual(result.RentCost, 0);
            Assert.AreEqual(result.RentCurrency, string.Empty);
            Assert.AreEqual(result.CarDesc, string.Empty);
            Assert.AreEqual(result.CarID, string.Empty);
            Assert.AreEqual(result.CarLogoImage, string.Empty);
            Assert.AreEqual(result.CarImage, string.Empty);
        }
    }
}