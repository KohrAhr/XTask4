﻿using System.Collections.ObjectModel;
using System.Data;
using System.Data.SqlClient;
using System.Reflection;

namespace Lib.Db.ServerSide
{
    public class CoreDbHelper : ICoreDbHelper
    {
        private string _dbConnectionString;

        public string ConnectionString
        { 
            get 
            { 
                return _dbConnectionString; 
            } 
            set
            {
                _dbConnectionString = value;
            }
        }

        public CoreDbHelper(string aDbConnectionString)
        {
            _dbConnectionString = aDbConnectionString;
        }

        public async Task<DataTable?> RunExecStatement(string aQuery)
        {
            DataTable? result = null;

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_dbConnectionString))
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
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Trace.Write($"SQL Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write($"Exception: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        ///     Run Scalar select statement
        /// </summary>
        /// <param name="aSql"></param>
        /// <returns></returns>
        public async Task<int> RunScalarExecStatement(string aSql)
        {
            int result = -1;

            try
            {
                using (IDbConnection dbConnection = new SqlConnection(_dbConnectionString))
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
            }
            catch (SqlException ex)
            {
                System.Diagnostics.Trace.Write($"SQL Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write($"Exception: {ex.Message}");
            }

            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="dataTable"></param>
        /// <returns></returns>
        public ObservableCollection<T> ConvertDataTableToObservableCollection<T>(DataTable? dataTable) where T : new()
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

                            SetPropertyValue(property, item, value);
                        }
                    }
                    collection.Add(item);
                }
            }

            return collection;
        }

        private void SetPropertyValue(PropertyInfo property, object item, object value)
        {
            try
            {
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
            catch (InvalidCastException ex)
            {
                System.Diagnostics.Trace.Write($"Invalid Cast Exception: {ex.Message}");
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write($"Exception: {ex.Message}");
            }
        }
    }
}