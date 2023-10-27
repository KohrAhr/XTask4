using System.Collections.ObjectModel;
using System.Reflection;
using Lib.Suppliers.Types;

namespace Lib.Suppliers.Functions
{
    public static class ConvertFunctions
    {
        /// <summary>
        ///     Convert data from any supplier class to our own class (db ready)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entry"></param>
        /// <returns></returns>
        public static SupplierCommon FromSupplier<T>(T entry) where T : new() 
        {
            SupplierCommon result = new();

            foreach (PropertyInfo sourceProperty in typeof(T).GetProperties())
            {
                SupplierBaseAttribute? supplierBaseAttribute = sourceProperty.GetCustomAttributes(typeof(SupplierBaseAttribute), false).FirstOrDefault() as SupplierBaseAttribute;
                if (supplierBaseAttribute == null)
                {
                    continue;
                }

                PropertyInfo? targetProperty = typeof(SupplierCommon).GetProperty(supplierBaseAttribute.TargetProperty);
                if (targetProperty == null)
                {
                    continue;
                }

                // Get and Set value finally
                object? value = sourceProperty.GetValue(entry);
                targetProperty.SetValue(result, value);
            }

            // Get Id by Type. Otehrwise 0 is Def
            Supplier? supplier = new SupplierHelper().SuppliersInfo.FirstOrDefault(x => x.ObjectType == typeof(T));
            if (supplier != null)
            {
                result.SupplierID = (int)supplier.Id;
            }

            return result;
        }

        /// <summary>
        ///     Proceed list of entries from one supplier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static ObservableCollection<SupplierCommon> FromSupplier<T>(ObservableCollection<T> entries) where T : new()
        {
            ObservableCollection<SupplierCommon> result = new();

            foreach (T entry in entries)
            {
                SupplierCommon destinationItem = FromSupplier<T>(entry);
                result.Add(destinationItem);
            }

            return result;
        }

        /// <summary>
        ///     Proceed list of entries from one supplier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static IEnumerable<SupplierCommon> FromSupplierEx<T>(ObservableCollection<T> entries) where T : new()
        {
            foreach (T entry in entries)
            {
                SupplierCommon destinationItem = FromSupplier<T>(entry);
                yield return destinationItem;
            }
        }

        /// <summary>
        ///     Proceed list of entries from one supplier
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="entries"></param>
        /// <returns></returns>
        public static IEnumerable<SupplierCommon> FromSupplierEx<T>(IEnumerable<T> entries) where T : new()
        {
            yield return (SupplierCommon)FromSupplierEx(new ObservableCollection<T>(entries));
        }
    }
}
