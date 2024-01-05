using System.Text.Json;

namespace GigaChatAdapter.Auth
{
    public class AuthorizationResponse
    {
        /// <summary>
        /// HTTP response
        /// </summary>
        public HttpResponseMessage HttpResponse { get; private set; }

        /// <summary>
        /// GigaChat response object from authorization service
        /// </summary>
        public GigaChatAuthorizationResponse? GigaChatAuthorizationResponse { get; private set; }

        /// <summary>
        /// Indicates request status. Success [true] or not [false]
        /// </summary>
        public bool AuthorizationSuccess { get; private set; }

        /// <summary>
        /// Message if request authorization failed. Else empty.
        /// </summary>
        public string ErrorTextIfFailed { get; private set; }

        /// <summary>
        /// Response from GigaChat authorization service
        /// </summary>
        /// <param name="HttpMsg">Http response</param>
        public AuthorizationResponse(HttpResponseMessage HttpMsg)
        {
            HttpResponse = HttpMsg;
            string responseVal = HttpMsg.Content.ReadAsStringAsync().Result;

            //auth successed
            if (HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                AuthorizationSuccess = true;
                GigaChatAuthorizationResponse = JsonSerializer.Deserialize<GigaChatAuthorizationResponse>(responseVal);
            }
            //auth failed
            else
            {
                AuthorizationSuccess = false;
                if (string.IsNullOrEmpty(responseVal))
                {
                    ErrorTextIfFailed = "See HttpResponse to get more information";
                }
                else
                {
                    ErrorTextIfFailed = responseVal;
                }
            }
        }
    }
}
