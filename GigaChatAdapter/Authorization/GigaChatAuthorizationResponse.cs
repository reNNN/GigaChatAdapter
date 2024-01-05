using System.Text.Json.Serialization;

namespace GigaChatAdapter.Auth
{
    public class GigaChatAuthorizationResponse
    {

        /// <summary>
        /// AccessToken for access to GigaChat API
        /// </summary>
        [JsonPropertyName("access_token")]
        public string AccessToken { get; set; }


        /// <summary>
        /// when AccessToken will expired (in milliseconds)
        /// </summary>
        [JsonPropertyName("expires_at")]
        public long? ExpiresAt { get; set; }

        private DateTime? _expiresAtDateTime = null;
        /// <summary>
        /// When AccessToken will expired (local dateTime)
        /// </summary>
        public DateTime? ExpiresAtDateTime {
            get {
                if (!ExpiresAt.HasValue)
                {
                    return null;
                }
                
                if (_expiresAtDateTime == null)
                {
                    TimeSpan ts = TimeSpan.FromMilliseconds(ExpiresAt.Value);
                    _expiresAtDateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc) + ts;
                    _expiresAtDateTime = _expiresAtDateTime.Value.ToLocalTime();
                }

                return _expiresAtDateTime.Value;
            } }
    }
}
