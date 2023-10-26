using Lib.Json;
using Lib.Suppliers.Types;
using Microsoft.AspNetCore.Mvc;
using ServiceCollector.Functions;
using System.Collections.ObjectModel;
using System.Data;

namespace ServiceCollector.Core
{
    public class CoreLogicUnitB
    {
        public async Task<ActionResult<string>> Main(float aMin, float aMax, string aCarType)
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
            string where = CoreHelper.BuildWhereCondition(aCarType.Trim(), aMin, aMax);

            runSql = String.Format(CONST_TEMPLATE_SQL, where);

            // Calc Crc32 for runSql
            // Run Query with Cache option and
            DataTable dataTable = await CoreCache.GetDataFromCacheOrDatabase<DataTable>(runSql);

            // Data Table to Observable collection
            ObservableCollection<SupplierCommon> data = CoreDbHelper.ConvertDataTableToObservableCollection<SupplierCommon>(dataTable);

            // Observable collection to Json
            string result = JsonHelper.ObservableCollectionToJson(data);

            // Way 2. Without cache
            //            ObservableCollection<SupplierCommon> items = CoreDbHelper.RunExecStatement<SupplierCommon>(runSql);

            // return result as Common Json
            return result;
        }
    }
}
