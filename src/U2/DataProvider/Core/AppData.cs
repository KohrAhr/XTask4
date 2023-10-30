namespace DataProvider
{
    public static class AppData
    {
        public const int CONST_DEF_MAX_RECORDS_IN_OUTPUT = 2000;

        /// <summary>
        ///     Connection string to MS SQL DBMS
        /// </summary>
        public static string ConnString = String.Empty;

        /// <summary>
        ///     Using in SELECT statement
        /// </summary>
        public static int MaxRecordsInOutput = 500;

        /// <summary>
        ///     Using only for internal cache
        /// </summary>
        public static int MinutesDbTTL = 15;
    }
}
