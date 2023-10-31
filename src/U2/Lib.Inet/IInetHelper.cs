namespace Lib.Inet
{
    public interface IInetHelper
    {
        Task<string> LoadRemoteData(string aUrl);

        Task<string> PostData(string aUrl, string jsonData = "");
    }
}
