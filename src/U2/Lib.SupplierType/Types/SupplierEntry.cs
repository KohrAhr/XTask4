namespace Lib.SupplierType.Types
{
    public class SupplierEntry
    {
        public int SupplierID { get; set; }

        public string SupplierName { get; set; } = string.Empty;

        public string SupplierUrl { get; set; } = string.Empty;

        public bool IsActive { get; set; }

        public bool IsAutoRefreshActive { get; set; }

        public int AutoRefreshInMinutes { get; set; }

        public string SupplierClassAsJson { get; set; } = string.Empty;

        public string ClassTransferList { get; set; } = string.Empty;
    }
}
