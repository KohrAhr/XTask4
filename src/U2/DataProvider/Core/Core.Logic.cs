using Microsoft.AspNetCore.Mvc;
using System.Collections.ObjectModel;
using System.Data;
using Lib.Json;
using Lib.Db.ServerSide;
using DataProvider.Functions;

namespace DataProvider.Core
{
    public class CoreLogic
    {
        private ICoreDbHelper CoreDbHelper { get; set; }

        public CoreLogic(ICoreDbHelper aCoreDbHelper)
        {
            CoreDbHelper = aCoreDbHelper;
        }

        public async Task<ActionResult<string>> Main<T>(int aSupplierId, float aMin, float aMax, string aCarType) where T : new()
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
            ObservableCollection<T> data = CoreDbHelper.ConvertDataTableToObservableCollection<T>(dataTable);

            // Observable collection to Json
            string result = JsonHelper.ObservableCollectionToJson(data);

            // return result as Common Json
            return result;
        }
    }
}
