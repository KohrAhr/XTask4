using System.Collections.ObjectModel;

namespace Lib.Json
{
    public interface IJsonHelper
    {
        T? JsonStringToClass<T>(string aValue);

        dynamic? JsonStringToClass(string aValue, Type aEntryType);

        dynamic? JsonToCommonCollection(string aData, Type aEntryType);

        string ObservableCollectionToJson<T>(ObservableCollection<T> collection);
    }
}
