using System.Text.Json.Serialization;

namespace GigaChatAdapter.Completions
{
    public class GigaChatChoice
    {
        /// <summary>
        /// Item index in Choice collection
        /// </summary>
        [JsonPropertyName("index")]
        public int Index { get; set; }

        /// <summary>
        /// Reason why the message was stopped
        /// </summary>
        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }

        /// <summary>
        /// Responsed message
        /// </summary>
        [JsonPropertyName("message")]
        public GigaChatMessage Message { get; set; }
    }
}
