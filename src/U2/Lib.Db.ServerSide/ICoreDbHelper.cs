using System.Collections.ObjectModel;
using System.Data;

namespace Lib.Db.ServerSide
{
    public interface ICoreDbHelper
    {
        string ConnectionString { get; set; }

        Task<DataTable?> RunExecStatement(string aQuery);

        Task<int> RunScalarExecStatement(string aSql);

        ObservableCollection<T> ConvertDataTableToObservableCollection<T>(DataTable? dataTable) where T : new();
    }
}
