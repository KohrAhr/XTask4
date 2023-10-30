using System.Data;
using Lib.Db.ServerSide;
using Lib.SupplierType.Types;

namespace DataLoader.Functions
{
    public static class CoreHelper
    {
        public static void ProceedConfigFile()
        {
            IConfigurationRoot config = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();

            // #1
            string? lValue = config["DbConnString"];

            if (string.IsNullOrEmpty(lValue))
            {
                System.Diagnostics.Trace.Write("Connection string to db cannot be empty");
                throw new Exception("Connection string to db cannot be empty");
            }

            AppData.ConnString = lValue;

            // #2
            int lResult;

            lValue = config["BatchInsertSize"];

            if (!int.TryParse(lValue, out lResult))
            {
                lResult = AppData.CONST_DEF_RECORDS_IN_BATCH_INSERT;
            }

            AppData.BatchInsertSize = lResult;
        }

        ///

        public static async Task LoadSuppliers()
        {
            string CONST_GET_SUPPLIERS = "select * from [Suppliers] where (IsActive = 1);";

            CoreDbHelper coreDbHelper = new CoreDbHelper(AppData.ConnString);
            DataTable dataTable = await coreDbHelper.RunExecStatement(CONST_GET_SUPPLIERS);

            AppData.supplierEntries = coreDbHelper.ConvertDataTableToObservableCollection<SupplierEntry>(dataTable);

            return;
        }

    }
}