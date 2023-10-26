using NUnit.Framework;
using Lib.Suppliers.Functions;
using Lib.Suppliers.Tests.TestData;
using Lib.Suppliers.Types;
using System.Collections.ObjectModel;

namespace Lib.Suppliers.Tests
{
    /// <summary>
    ///     SouthRentals -- is #3
    /// </summary>
    public class Tests_SouthRentals_Collection
    {
        [SetUp]
        public void Setup()
        {
        }

        [Test]
        public void ValidCollection_List1_SouthRentals()
        {
            ObservableCollection<SupplierCommon> result = ConvertFunctions.FromSupplier<SouthRentalsIncommingEntry>(new ObservableCollection<SouthRentalsIncommingEntry>(new() { SouthRentailsEntries.item1, SouthRentailsEntries.item2 }));

            Assert.AreEqual(result.Count, 2);
        }

        [Test]
        public void ValidCollection_DefValue_SouthRentals()
        {
            ObservableCollection<SupplierCommon> result = ConvertFunctions.FromSupplier<SouthRentalsIncommingEntry>(new ObservableCollection<SouthRentalsIncommingEntry>());

            Assert.AreEqual(result.Count, 0);
        }
    }
}