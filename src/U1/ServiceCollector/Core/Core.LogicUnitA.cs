using Lib.Inet;
using Lib.Json;
using Lib.Suppliers.Functions;
using Lib.Suppliers.Types;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;

namespace ServiceCollector.Core
{
    public class CoreLogicUnitA
    {
        private const string CONST_FAIL = "Error";

        public async Task<ActionResult<string>> Main(int aPID)
        {
            IEnumerable<SupplierCommon>? data = await ResolveLatestData(aPID);

            return DeleteInsert(data, aPID);
        }

        /// <summary>
        ///     1. Get Json from Remote server
        ///     2. Convert them to Common format
        /// </summary>
        /// <param name="aPID">
        ///     Supplier Id, basically Id of Server
        /// </param>
        /// <returns></returns>
        public async Task<IEnumerable<SupplierCommon>?> ResolveLatestData(int aPID)
        {
            // Id is Type
            Supplier? supplier = SupplierHelper.SuppliersInfo.FirstOrDefault(x => (int)x.Id == aPID);
            if (supplier == null || supplier.ObjectType == null || String.IsNullOrEmpty(supplier.Url))
            {
                return null;
            }

            // Get Url by Id
            // Make Async Get request to remote Url
            string rawResult = await InetHelper.LoadRemoteData(supplier.Url);
            if (String.IsNullOrEmpty(rawResult))
            {
                return null;
            }

            // Get object type we need
            // EntryType variable contain Type of the Object
            dynamic? supplierResult = JsonHelper.JsonToCommonCollection(rawResult, supplier.ObjectType);
            if (supplierResult == null)
            {
                return null;
            }

            // Convert to our own format
            IEnumerable<SupplierCommon>? itemsCommon = ConvertFunctions.FromSupplierEx(supplierResult);
            return itemsCommon;
        }


        /// <summary>
        ///     Delete and Insert new
        /// </summary>
        /// <param name="itemsCommon"></param>
        /// <param name="aPID">Supplier Id, basically Id of Server</param>
        /// <returns>
        ///     "OK:..." if data has been fetched from remote server and successfully stored in db
        ///     CONST_FAIL otherwise
        /// </returns>
        public ActionResult<string> DeleteInsert(IEnumerable<SupplierCommon>? itemsCommon, int aPID)
        {
            if (itemsCommon == null)
            {
                return CONST_FAIL;
            }

            // Time to make changes in db.
            IDbTransaction? transaction = null;
            try
            {
                using (IDbConnection dbConnection = new SqlConnection(AppData.ConnString))
                {
                    dbConnection.Open();

                    if (dbConnection.State != ConnectionState.Open)
                    {
                        return CONST_FAIL;
                    }

                    transaction = dbConnection.BeginTransaction(IsolationLevel.ReadUncommitted);

                    // Save (delete old & insert new) result to DB
                    dbConnection.BulkDelete(transaction, aPID);
                    dbConnection.BulkInsert(transaction, itemsCommon, AppData.BatchInsertSize);
                    transaction?.Commit();
                }
            }
            catch (Exception ex)
            {
                transaction?.Rollback();
                System.Diagnostics.Trace.Write(ex.ToString());
                return CONST_FAIL;
            }

            // Return the result
            return $"OK:" + itemsCommon.Count().ToString();
        }
    }
}