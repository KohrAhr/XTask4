using Lib.Json;
using Lib.SupplierType;
using Microsoft.AspNetCore.Mvc;
using DataProvider.Functions;
using System.Collections.ObjectModel;
using System.Data;
using Lib.Db.ServerSide;

namespace DataProvider.Core
{
    public class CoreLogic
    {
        public async Task<ActionResult<string>> Main(int aSupplierId, float aMin, float aMax, string aCarType)
        {
            // Build SQL
            string runSql;
            const string CONST_TEMPLATE_SQL =
                "SELECT TOP 1000 " +
                "   [SupplierID], [SupplierOfferID], [RentCost], [RentCurrency], [CarDesc], [CarID], [CarLogoImage], [CarImage], [EntryDateTime] " +
                "FROM " +
                "   [Offers]" +
                "{0}" +
                "order by" +
                "   [RentCost], 1;";

            // Build where condition
            string where = CoreHelper.BuildWhereCondition(aSupplierId, aCarType.Trim(), aMin, aMax);

            runSql = String.Format(CONST_TEMPLATE_SQL, where);

            // Calc Crc32 for runSql
            // Run Query with Cache option and
            DataTable dataTable = await CoreCache.GetDataFromCacheOrDatabase<DataTable>(runSql);

            // Data Table to Observable collection
            CoreDbHelper coreDbHelper = new CoreDbHelper(AppData.ConnString);
            ObservableCollection<SupplierCommon> data = coreDbHelper.ConvertDataTableToObservableCollection<SupplierCommon>(dataTable);

            // Observable collection to Json
            string result = JsonHelper.ObservableCollectionToJson(data);

            // return result as Common Json
            return result;
        }
    }
}
