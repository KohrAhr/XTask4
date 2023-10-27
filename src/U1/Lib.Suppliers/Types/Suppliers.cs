using Microsoft.Extensions.Configuration;

namespace Lib.Suppliers.Types
{
    public enum Suppliers
    {
        spUnknown = 0,
        spBestRentals = 1,
        spNothernRentals = 2,
        spSouthRentals = 3
    }

    public class Supplier
    {
        public string Name { get; set; } = string.Empty;
        public Suppliers Id { get; set; }
        public Type? ObjectType { get; set; } = null;
        public string Url { get; set; } = string.Empty;
    }

    public class SupplierHelper : ISupplierHelper
    {
        private List<Supplier> suppliersInfo { get; set; }

        public List<Supplier> SuppliersInfo
        { 
            get
            {
                return suppliersInfo;
            }
            set
            {
                suppliersInfo = value;
            }
        }

        public IConfigurationBuilder configBuilder;
        public IConfigurationRoot config;

        /// <summary>
        ///     Constructor
        /// </summary>
        public SupplierHelper() 
        {
            // Read Config file?
            configBuilder = new ConfigurationBuilder().AddJsonFile("appsettings.json", true, true);
            config = configBuilder.Build();

            //
            suppliersInfo = GenerateSupplierInfoArray();
        }

        private List<Supplier> GenerateSupplierInfoArray()
        {
            List<Supplier> suppliersInfo = new()
            {
                new Supplier
                {
                    Name = "Best Rentals",
                    Id = Suppliers.spBestRentals,
                    ObjectType = typeof(BestRentalsIncommingEntry),
                    Url = config[((int)Suppliers.spBestRentals).ToString()],
                },

                new Supplier
                {
                    Name = "Northern Rentals",
                    Id = Suppliers.spNothernRentals,
                    ObjectType = typeof(NorthernRentalsIncommingEntry),
                    Url = config[((int)Suppliers.spNothernRentals).ToString()],
                },

                new Supplier
                {
                    Name = "South Rentals",
                    Id = Suppliers.spSouthRentals,
                    ObjectType = typeof(SouthRentalsIncommingEntry),
                    Url = config[((int)Suppliers.spSouthRentals).ToString()],
                }
            };

            return suppliersInfo;
        }
    }
}
