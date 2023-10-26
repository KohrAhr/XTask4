namespace ServiceCollector
{
    public static class AppData
    {
        public const int CONST_DEF_BATCH_INSERT_SIZE = 10;

        /// <summary>
        ///     
        /// </summary>
        public static List<string> AllowedIPs = new();

        /// <summary>
        ///     Connection string to MS SQL DBMS
        /// </summary>
        public static string ConnString = String.Empty;

        public static int BatchInsertSize = 10;

        public static int MinutesDbTTL = 15;
    }
}
