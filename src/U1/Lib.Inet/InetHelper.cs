namespace Lib.Inet
{
    public static class InetHelper
    {
        /// <summary>
        ///     Get request
        /// </summary>
        /// <param name="aUrl">
        ///     Url of remote server
        /// </param>
        /// <returns>Json or Empty string in case of error + throw exception</returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> LoadRemoteData(string aUrl)
        {
            string result = string.Empty;

            try
            {
                using (HttpClient httpClient = new())
                {
                    using (HttpResponseMessage response = await httpClient.GetAsync(aUrl))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            using (HttpContent content = response.Content)
                            {
                                result = await content.ReadAsStringAsync();
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                // ?? I am not sure yet

                System.Diagnostics.Trace.Write(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

            return result;
        }
    }
}