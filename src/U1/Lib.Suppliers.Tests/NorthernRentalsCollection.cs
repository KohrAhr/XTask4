using NUnit.Framework;
using Lib.Suppliers.Tests.TestData;
using Lib.Suppliers.Functions;
using Lib.Suppliers.Types;
using System.Collections.ObjectModel;

namespace Lib.Suppliers.Tests
{
    /// <summary>
    /// </summary>
    public class Tests_NorthernRentals_Collection
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidCollection_List1_NorthernRentals()
        {
            ObservableCollection<SupplierCommon> result = ConvertFunctions.FromSupplier<NorthernRentalsIncommingEntry>(new ObservableCollection<NorthernRentalsIncommingEntry>(new() { NorthernRentalsEntries.item1, NorthernRentalsEntries.item2 }));

            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void ValidCollection_DefValue_NorthernRentals()
        {
            ObservableCollection<SupplierCommon> result = ConvertFunctions.FromSupplier<NorthernRentalsIncommingEntry>(new ObservableCollection<NorthernRentalsIncommingEntry>());

            Assert.AreEqual(result.Count, 0);
        }
    }
}