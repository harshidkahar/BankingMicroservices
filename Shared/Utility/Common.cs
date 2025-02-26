using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http.Headers;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Utility
{
    public class Common
    {
        public static HttpClient BuildPostWebRequestNew(string p_URL, string p_Data, Cookie authCookie, string p_ContentType, int p_Timeout, bool p_UseCredentials, string p_Username, string p_Password, string clientId, string clientSecret, string authKey, string body)
        {
            ASCIIEncoding encodedData = new ASCIIEncoding();
            byte[] byteArray = encodedData.GetBytes(p_Data);
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue(p_ContentType));
            httpClient.DefaultRequestHeaders.Add("client-id", clientId);
            if (clientSecret != null)
            {
                httpClient.DefaultRequestHeaders.Add("client-secret", clientSecret);
            }
            if (authKey != null)
            {
                httpClient.DefaultRequestHeaders.Add("Authorization", "Bearer " + authKey);
            }

            //httpClient.DefaultRequestHeaders.Add("origin", "https://www.test.com");
            return httpClient;
        }
    }
}
