using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace ServiceCollector.Core
{
    public static class CoreDbHelper
    {
        public static async Task<DataTable> RunExecStatement(string aQuery)
        {
            DataTable result = null;

            using (IDbConnection dbConnection = new SqlConnection(AppData.ConnString))
            {
                dbConnection.Open();

                using (SqlCommand sqlCommand = new SqlCommand(aQuery, (SqlConnection)dbConnection))
                {
                    result = new DataTable();
                    using (SqlDataAdapter sqlDataAdapter = new SqlDataAdapter(sqlCommand))
                    {
                        await Task.Run(() =>
                        {
                            sqlDataAdapter.Fill(result);
                        });
                    }
                }
            }

            return result;
        }

        /// <summary>
        ///     Run Scalar select statement
        /// </summary>
        /// <param name="aSql"></param>
        /// <returns></returns>
        public static async Task<int> RunScalarExecStatement(string aSql)
        {
            int result = -1;

            using (IDbConnection dbConnection = new SqlConnection(AppData.ConnString))
            {
                dbConnection.Open();
                using (SqlCommand sqlCommand = new SqlCommand(aSql, (SqlConnection)dbConnection))
                {
                    object? scalarResult = await sqlCommand.ExecuteScalarAsync();
                    if (scalarResult != null)
                    {
                        result = (int)scalarResult;
                    }
                }
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public static ObservableCollection<T> ConvertDataTableToObservableCollection<T>(DataTable? dataTable) where T : new()
        {
            ObservableCollection<T> collection = new ObservableCollection<T>(); 
            
            if (dataTable != null)
            {
                PropertyInfo[] properties = typeof(T).GetProperties();

                foreach (DataRow row in dataTable.Rows)
                {
                    T item = new T();

                    foreach (PropertyInfo property in properties)
                    {
                        string propertyName = property.Name;

                        if (dataTable.Columns.Contains(propertyName))
                        {
                            object value = row[propertyName];

                            if (property.PropertyType.IsGenericType && property.PropertyType.GetGenericTypeDefinition() == typeof(Nullable<>))
                            {
                                // Property is nullable
                                if (value == DBNull.Value)
                                {
                                    property.SetValue(item, null);
                                }
                                else
                                {
                                    property.SetValue(item, Convert.ChangeType(value, Nullable.GetUnderlyingType(property.PropertyType)));
                                }
                            }
                            else
                            {
                                // Property is non-nullable
                                if (value != DBNull.Value)
                                {
                                    property.SetValue(item, Convert.ChangeType(value, property.PropertyType));
                                }
                            }
                        }
                    }
                    collection.Add(item);
                }
            }

            return collection;
        }

    }
}
