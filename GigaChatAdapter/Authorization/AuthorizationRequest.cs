namespace GigaChatAdapter.Auth
{
    /// <summary>
    /// Model for authorization request to GigaChat API
    /// </summary>
    public class AuthorizationRequest
    {
        /// <summary>
        /// GUID for authorization request. Just for request identity only
        /// </summary>
        public Guid RqUID { get; private set; } = Guid.NewGuid();

        /// <summary>
        /// Authorization data. Get it in personal account on https://developers.sber.ru/ using workspace 
        /// </summary>
        public string AuthorizationID { get; private set; }

        /// <summary>
        /// Rate scope. Check it in personal account in https://developers.sber.ru/ in your workspace 
        /// </summary>
        public RateScope RateScope { get; private set; }

        /// <summary>
        /// Create AuthorizationRequest instance
        /// </summary>
        /// <param name="AuthorizationID">Authorization data</param>
        /// <param name="RateScope">Rate scope</param>
        /// <param name="RequestID">Authorization request ID</param>
        public AuthorizationRequest(string AuthorizationID, RateScope RateScope, Guid? RequestID = null)
        {
            RqUID = RequestID ?? Guid.NewGuid();
            this.AuthorizationID = AuthorizationID;
            this.RateScope = RateScope;
        }
    }
}
