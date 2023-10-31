using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;
using System.Data;
using Lib.SupplierType;
using DataLoader.Functions;
using Lib.SupplierType.Types;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace DataLoader.Core
{
    public class CoreLogic
    {
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
            // Load Suppliers info if needed
            if (AppData.supplierEntries == null)
            {
                await CoreHelper.LoadSuppliers();

                // Ok now?
                if (AppData.supplierEntries == null)
                {
                    return null;
                }
            }

            // Get supplier Url
            SupplierEntry? supplierEntry = AppData.supplierEntries.Where(x => x.SupplierID == aPID).FirstOrDefault();
            if (supplierEntry == null) 
            {
                return null;
            }

            if (AppData.InetHelper == null)
            {
                throw new ArgumentNullException(nameof(AppData.InetHelper));
            }

            // Get Json from remote server
            // Make Async Get request to remote Url
            string rawResult = await AppData.InetHelper.LoadRemoteData(supplierEntry.SupplierUrl);
            if (String.IsNullOrEmpty(rawResult))
            {
                return null;
            }

            // Create a list of transformations
            Dictionary<string, string> transformations = new Dictionary<string, string>();

            //
            List<Transformation> transformationList = JsonConvert.DeserializeObject<List<Transformation>>(supplierEntry.ClassTransferList);

            if (transformationList == null)
            {
                return null;
            }

            foreach (Transformation transformation in transformationList)
            {
                transformations[transformation.OldName] = transformation.NewName;
            }

            // Deserialize the JSON string into a JArray
            JArray jsonArray = JArray.Parse(rawResult);

            // Iterate through the array and modify the property names
            foreach (JObject jsonObject in jsonArray)
            {
                var propertiesToRename = jsonObject.Properties().ToList(); // Create a copy of properties to avoid issues when renaming

                foreach (var property in propertiesToRename)
                {
                    if (transformations.TryGetValue(property.Name, out var newName))
                    {
                        jsonObject[newName] = property.Value;
                        jsonObject.Remove(property.Name);
                    }
                }
            }

            // Make transformation into our own format
            IEnumerable<SupplierCommon>? itemsCommon = JsonConvert.DeserializeObject<List<SupplierCommon>>(jsonArray.ToString()).Select
            (
                item =>
                {
                    item.SupplierID = aPID;
                    return item;
                }
            );
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
                return AppData.CONST_FAIL;
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
                        return AppData.CONST_FAIL;
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
                return AppData.CONST_FAIL;
            }

            // Return the result
            return $"OK:" + itemsCommon.Count().ToString();
        }
    }
}