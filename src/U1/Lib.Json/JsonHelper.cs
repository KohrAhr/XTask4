using Newtonsoft.Json;
using System.Collections.ObjectModel;
using System.Reflection;

namespace Lib.Json
{
    public static class JsonHelper
    {
        /// <summary>
        ///     String to Class
        /// </summary>
        /// <typeparam name="T">Type of class</typeparam>
        /// <param name="aValue">Source data in JSON</param>
        /// <returns>Instance of Class with data</returns>
        public static T? JsonStringToClass<T>(string aValue)
        {
            try
            {
                // Load data from value to Class
                T rootEntry = JsonConvert.DeserializeObject<T>(aValue);

                return rootEntry;
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                return default;
            }
        }


        /// <summary>
        ///     Json String to Class.
        ///     New way
        /// </summary>
        /// <param name="aValue"></param>
        /// <param name="aEntryType"></param>
        /// <returns></returns>

        // +2h by AZ
        // Improved after consultation

        public static dynamic? JsonStringToClass(string aValue, Type aEntryType)
        {
            if (string.IsNullOrEmpty(aValue))
            {
                return null;
            }

            Type type = typeof(JsonConvert);
            MethodInfo? genericMethod = type.GetMethods().FirstOrDefault(x => x.Name == "DeserializeObject" && x.IsGenericMethodDefinition);
            if (genericMethod == null)
            {
                return null;
            }

            MethodInfo specificMethod = genericMethod.MakeGenericMethod(aEntryType);
            dynamic? result = null;

            try
            {
                result = specificMethod.Invoke(null, new object[] { aValue });
            }
            catch (Exception ex) 
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                return null;
            }

            return result;
        }

        /// <summary>
        ///     Convert Suppliers specific Json to common format
        /// </summary>
        /// <param name="aData">Data in Json. Set of records []</param>
        /// <param name="aEntryType">aEntryType variable contain Type of the Object</param>
        /// <returns>
        ///     Null if nothing to convert or cannot be converted
        /// </returns>
        public static dynamic? JsonToCommonCollection(string aData, Type aEntryType)
        {
            Type genericType = typeof(ObservableCollection<>).MakeGenericType(aEntryType);
            dynamic? itemsOriginal = JsonStringToClass(aData, genericType);

            return itemsOriginal;
        }

        public static string ObservableCollectionToJson<T>(ObservableCollection<T> collection)
        {
            string result = JsonConvert.SerializeObject(collection);

            return result;
        }
    }
}