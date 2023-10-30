using System.Text;

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
                using (HttpClientHandler handler = new HttpClientHandler())
                {                // Configure the handler to ignore SSL certificate errors
                    handler.ServerCertificateCustomValidationCallback = (sender, cert, chain, sslPolicyErrors) => true;

                    using (HttpClient httpClient = new HttpClient(handler))
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
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                throw new HttpRequestException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                // ?? I am not sure yet

                System.Diagnostics.Trace.Write(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

        /// <summary>
        ///     
        /// </summary>
        /// <param name="aUrl"></param>
        /// <param name="jsonData"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public static async Task<string> PostData(string aUrl, string jsonData = "")
        {
            string result = string.Empty;

            try
            {
                using (HttpClient httpClient = new())
                {
                    StringContent content = new StringContent(jsonData, Encoding.UTF8, "application/json");

                    using (HttpResponseMessage response = await httpClient.PostAsync(aUrl, content))
                    {
                        if (response.IsSuccessStatusCode)
                        {
                            result = await response.Content.ReadAsStringAsync();
                        }
                    }
                }
            }
            catch (InvalidOperationException ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                throw new InvalidOperationException(ex.Message, ex);
            }
            catch (HttpRequestException ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                throw new HttpRequestException(ex.Message, ex);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Trace.Write(ex.ToString());
                throw new Exception(ex.Message, ex);
            }

            return result;
        }

    }
}