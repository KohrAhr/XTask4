using ServiceCollector.Functions;
using System;
using System.Data;
using System.Runtime.Caching;

namespace ServiceCollector.Core
{
    public static class CoreCache
    {
        /// <summary>
        ///     We are using Cache only for DataTable objects
        /// </summary>
        private static MemoryCache _cache = new MemoryCache("AppDataTableContext");

        public static async Task<T> GetDataFromCacheOrDatabase<T>(string sqlQuery, string objectName = "") where T : new()
        {
            // Calc CRC32 for SqlQuery string? And use CRC as Name?
            if (string.IsNullOrEmpty(objectName))
            {
                objectName = sqlQuery;
            }

            // We are using CRC32 hash sum as name :)
            string newObjectName = CoreHelper.CalculateCRC32(objectName).ToString();

            // If the data is in the cache, return it
            if (_cache.Contains(newObjectName) && _cache[newObjectName] is T cachedObject)
            {
                return cachedObject;
            }

            T dataResult = new T();
            object? x;

            // If the data is not in the cache, fetch it from the database

            // Type DataTable -- regular sql query
            if (typeof(T) == typeof(DataTable))
            {
                x = await CoreDbHelper.RunExecStatement(sqlQuery);

                // No, cannot be null here. V3022
                //if (x != null)
                //{
                dataResult = (T)x;
//                }
            }
            else
            // Type Int -- Scalar query
            if (typeof(T) == typeof(Int64) || typeof(T) == typeof(Int32) || typeof(T) == typeof(Int16) || typeof(T) == typeof(int))
            {
                x = await CoreDbHelper.RunScalarExecStatement(sqlQuery);

                // No, cannot be null here. V3022
                //if (x != null)
                //{
                dataResult = (T)x;
//                }
            }
            else
            {
                // Other type ?
                throw new NotImplementedException();
            }

            // No, cannot be null here. V3022
            //if (dataResult != null)
            //{
                // Store the data in the cache with an expiration time
                CacheItemPolicy policy = new CacheItemPolicy
                {
                    // XX minutes
                    AbsoluteExpiration = DateTimeOffset.Now.AddMinutes(AppData.MinutesDbTTL)
                };
                _cache.Add(newObjectName, dataResult, policy);
//            }

            return dataResult;
        }
    }
}
