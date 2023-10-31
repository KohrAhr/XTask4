using System.Collections.ObjectModel;
using Lib.Db.ServerSide;
using Lib.SupplierType.Types;

namespace DataLoader
{
    public static class AppData
    {
        public const string CONST_FAIL = "Error";

        public const int CONST_DEF_RECORDS_IN_BATCH_INSERT = 1000;

        /// <summary>
        ///     Connection string to MS SQL DBMS
        /// </summary>
        public static string ConnString = String.Empty;


        /// <summary>
        ///     Max rows count for Batch Sql Insert
        /// </summary>
        public static int BatchInsertSize = CONST_DEF_RECORDS_IN_BATCH_INSERT;

        /// <summary>
        /// 
        /// </summary>
        public static ObservableCollection<SupplierEntry>? supplierEntries = null;

        public static string PostRequestToResetCache = string.Empty;

        public static ICoreDbHelper? CoreDbHelper = null;
    }
}
