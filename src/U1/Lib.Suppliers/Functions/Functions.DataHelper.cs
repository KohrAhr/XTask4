using System.Data;
using System.Data.SqlClient;
using System.Reflection;
using Lib.Suppliers.Types;

namespace Lib.Suppliers.Functions
{
    public static class DataHelperFunctions
    {
        /// <summary>
        ///     Create in memory DataTable with the same fields like in real db
        /// </summary>
        /// <returns></returns>
        public static DataTable CreateCommonInMemoryTable()
        {
            DataTable result = new();

            // Way 1

            result.Columns.Add("SupplierID", typeof(int));
            result.Columns.Add("SupplierOfferID", typeof(string));
            result.Columns.Add("RentCost", typeof(float));
            result.Columns.Add("RentCurrency", typeof(string));
            result.Columns.Add("CarDesc", typeof(string));
            result.Columns.Add("CarID", typeof(string));
            result.Columns.Add("CarLogoImage", typeof(string));
            result.Columns.Add("CarImage", typeof(string));

            // Way 2. We know class SupplierCommon. Use it. Create new attribute if needed
            // TODO: !

            //foreach (PropertyInfo sourceProperty in typeof(SupplierCommon).GetProperties())
            //{
            //    Type columnType = sourceProperty.PropertyType;
            //    result.Columns.Add(sourceProperty.Name, columnType);
            //}

            //

            return result;
        }

        #region Extension method for Table

        /// <summary>
        /// </summary>
        /// <param name="aTable"></param>
        /// <param name="aRecords"></param>
        /// <returns></returns>
        public static void FillData(this DataTable aTable, IEnumerable<SupplierCommon> aRecords)
        {
            foreach (SupplierCommon record in aRecords)
            {
                aTable.Rows.Add(
                    record.SupplierID,
                    record.SupplierOfferID,
                    record.RentCost,
                    record.RentCurrency,
                    record.CarDesc,
                    record.CarID,
                    record.CarLogoImage,
                    record.CarImage
                );
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="records"></param>
        public static void BulkInsert(this IDbConnection aConnection, IDbTransaction aDbTransaction, IEnumerable<SupplierCommon> records, int aBatchSize = 10)
        {
            DataTable dataTable = CreateCommonInMemoryTable();

            // Insert all data into in-memory table
            dataTable.FillData(records);

            using (SqlBulkCopy bulkCopy = new SqlBulkCopy((SqlConnection)aConnection,SqlBulkCopyOptions.Default, (SqlTransaction)aDbTransaction))
            {
                // Hardcoded 1
                bulkCopy.DestinationTableName = "Offers";
                bulkCopy.BatchSize = aBatchSize;

                foreach (DataColumn column in dataTable.Columns)
                {
                    bulkCopy.ColumnMappings.Add(column.ColumnName, column.ColumnName);
                }

                bulkCopy.WriteToServer(dataTable);
            }
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="aConnection"></param>
        /// <param name="aDbTransaction"></param>
        /// <param name="aSupplierId"></param>
        /// <returns></returns>
        public static int BulkDelete(this IDbConnection aConnection, IDbTransaction? aDbTransaction, int aSupplierId)
        {
            // Hardcoded 2
            string CONST_DELETE_SQL = $"delete from [Offers] where (SupplierID = {aSupplierId});";

            int result = 0;

            using (IDbCommand command = aConnection.CreateCommand()) 
            {
                command.Transaction = aDbTransaction;
                command.CommandText = CONST_DELETE_SQL;
                result = command.ExecuteNonQuery();
            }

            return result;
        }

        #endregion Extension method for Table
    }
}
