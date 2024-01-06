using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GigaChatAdapter.Completions
{
    /// <summary>
    /// Data for completion request
    /// </summary>
    public class CompletionRequest
    {
        /// <summary>
        /// Access token to authorize
        /// </summary>
        public string AccessToken { get; private set; }

        /// <summary>
        /// Content media type. Default value 'application/json'
        /// </summary>
        public string ContentType { get; set; } = "application/json";

        /// <summary>
        /// Data to send to AI model
        /// </summary>
        public GigaChatCompletionRequest RequestData { get; set; }

        /// <summary>
        /// Create request with one message
        /// </summary>
        /// <param name="AccessToken">Access token.Requiered.</param>
        /// <param name="Prompt">Message to request AI</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CompletionRequest(string AccessToken, string Prompt, CompletionSettings settings) : this(AccessToken, new List<GigaChatMessage>(), settings)
        {
            RequestData.MessageCollection = new List<GigaChatMessage>()
            {
                new GigaChatMessage()
                {
                    Content = Prompt,
                    Role = CompletionRolesEnum.user.ToString()
                }
            };
        }

        /// <summary>
        /// Create request with message history
        /// </summary>
        /// <param name="AccessToken">Access token.Requiered</param>
        /// <param name="MessageHistory">Message history for sending to AI</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CompletionRequest(string AccessToken, IEnumerable<GigaChatMessage> MessageHistory, CompletionSettings settings)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                throw new ArgumentNullException(nameof(AccessToken));
            }

            this.AccessToken = AccessToken;

            RequestData = new GigaChatCompletionRequest() { MessageCollection = MessageHistory };

            if (settings != null)
            {
                RequestData.Model = settings.Model;
                RequestData.Temperature = settings.Temperature;
                RequestData.TopP = settings.TopP;
                RequestData.Count = settings.Count;
            }
        }

        /// <summary>
        /// Create request with custom RequestData and custom settings
        /// </summary>
        /// <param name="AccessToken">Access token.Requiered.</param>
        /// <param name="RequestData">Request data</param>
        /// <exception cref="ArgumentNullException"></exception>
        public CompletionRequest(string AccessToken, GigaChatCompletionRequest RequestData)
        {
            if (string.IsNullOrEmpty(AccessToken))
            {
                throw new ArgumentNullException(nameof(AccessToken));
            }

            this.AccessToken = AccessToken;
            this.RequestData = RequestData;
        }
    }
}
