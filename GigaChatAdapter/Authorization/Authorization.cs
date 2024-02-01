using GigaChatAdapter.Auth;

namespace GigaChatAdapter
{
    /// <summary>
    /// Class for authorization in GigaChat API
    /// Check API description https://developers.sber.ru/docs/ru/gigachat/api/authorization
    /// </summary>
    public class Authorization
    {
        /// <summary>
        /// Last response after request to Authentication service. Use SendRequest() if it is null
        /// </summary>
        public AuthorizationResponse LastResponse { get; private set; }

        /// <summary>
        /// Last request to Authentication service. Use SendRequest() if it is null
        /// </summary>
        public AuthorizationRequest LastRequest { get; private set; }

        /// <summary>
        /// Init Authorization instance to GigaChat Authorization service
        /// </summary>
        /// <param name="AuthorizationID">Authorization data</param>
        /// <param name="RateScope">Rate scope</param>
        /// <param name="RequestID">Authorization request ID (not required)</param>
        public Authorization(string AuthorizationID, RateScope RateScope, Guid? RequestID = null)
        {
            LastRequest = new AuthorizationRequest(AuthorizationID, RateScope, RequestID);
        }

        /// <summary>
        /// Send request for authorization
        /// </summary>
        /// <returns>Authorization response</returns>
        public async Task<AuthorizationResponse> SendRequest()
        {
            HttpClient client = new HttpClient();
            //Create headers
            client.DefaultRequestHeaders.Add(Settings.RequestConstants.AuthorizationHeaderTitle, $"Bearer {LastRequest.AuthorizationID}");
            client.DefaultRequestHeaders.Add(Settings.RequestConstants.RequestIDHeaderTitle, LastRequest.RqUID.ToString());

            //Create body (data)
            var data = new[]
            {
                    new KeyValuePair<string, string>(Settings.RequestConstants.RateScope, LastRequest.RateScope.ToString())
                };

            //send request and get response
            var httpResponse = await client.PostAsync(Settings.EndPoints.AuthorizationURL, new FormUrlEncodedContent(data));
            var response = new AuthorizationResponse(httpResponse);
            LastResponse = response;

            return response;
        }

        /// <summary>
        /// Send request to updateToken if it expired
        /// </summary>
        /// <param name="RqUID">Authorization request ID</param>
        /// <param name="force">Set True to update token anyway (expiresAt value ignore). Set False to update token if it is expired only</param>
        /// <param name="reserveTime">Timespan for updating before token will be expired. Default value = null (convert to TimeSpan.Zero)</param>
        /// <returns>Authorization response</returns>
        public async Task<AuthorizationResponse> UpdateToken(Guid? RqUID = null, bool force = false, TimeSpan? reserveTime = null)
        {
            TimeSpan expiredTimeSpan = reserveTime ?? TimeSpan.Zero;

            if (force || LastResponse == null || LastResponse.GigaChatAuthorizationResponse?.ExpiresAtDateTime - expiredTimeSpan < DateTime.Now)
            {
                Guid _rqUID = RqUID ?? Guid.NewGuid();
                LastRequest = new AuthorizationRequest(LastRequest.AuthorizationID, LastRequest.RateScope, _rqUID);
                LastResponse = await SendRequest();

                return LastResponse;
            }
            else
                return null;
        }
    }
}