using System.Text.Json.Serialization;

namespace GigaChatAdapter.Completions
{
    public class GigaChatCompletionResponse
    {
        /// <summary>
        /// List of responsed choices
        /// </summary>
        [JsonPropertyName("choices")]
        public IEnumerable<GigaChatChoice> Choices { get; set; }

        /// <summary>
        /// When responsed created. Unix time
        /// </summary>
        [JsonPropertyName("created")]
        public long Created { get; set; }

        /// <summary>
        /// Model that creates response
        /// </summary>
        [JsonPropertyName("model")]
        public string Model { get; set; }

        /// <summary>
        /// Token usage information
        /// </summary>
        [JsonPropertyName("usage")]
        public GigaChatUsage Usage { get; set; }

        /// <summary>
        /// Called method name
        /// </summary>
        [JsonPropertyName("object")]
        public string Obj { get; set; }
    }
}
