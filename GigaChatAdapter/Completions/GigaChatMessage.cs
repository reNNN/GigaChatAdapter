using System.Text.Json.Serialization;

namespace GigaChatAdapter.Completions
{
    /// <summary>
    /// Message object
    /// </summary>
    public class GigaChatMessage
    {
        /// <summary>
        /// Message author role
        /// </summary>
        [JsonPropertyName("role")]
        public string Role { get; set; }

        /// <summary>
        /// Message content
        /// </summary>
        [JsonPropertyName("content")]
        public string Content { get; set; }
    }
}
