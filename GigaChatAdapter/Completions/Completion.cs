using GigaChatAdapter.Completions;
using System.Text.Json;

namespace GigaChatAdapter
{
    public class Completion
    {
        public CompletionRequest LastRequest { get; private set; }

        public CompletionResponse LastResponse { get; private set; }

        /// <summary>
        /// Session message history
        /// </summary>
        public List<GigaChatMessage> History { get; set; } = new List<GigaChatMessage>();

        /// <summary>
        /// Send prompt
        /// </summary>
        /// <param name="Token">Access token</param>
        /// <param name="Message">Prompt</param>
        /// <param name="useHistory">if True - send chat history to GigaChat and save result in history (for session). If False - not use chat history and not save results</param>
        /// <returns></returns>
        public async Task<CompletionResponse> SendRequest(string Token, string Message, bool useHistory = true)
        {
            CompletionRequest request = null;

            if (useHistory)
            {
                History.Add(new GigaChatMessage()
                {
                    Content = Message,
                    Role = CompletionRolesEnum.user.ToString()
                });

                request = new CompletionRequest(Token, History);
            }
            else
            {
                request = new CompletionRequest(Token, Message);
            }

            LastRequest = request;
            return await SendRequestToService(request, useHistory);
        }

        private async Task<CompletionResponse> SendRequestToService(CompletionRequest request, bool useHistory)
        {
            HttpClient client = new HttpClient();

            //Create headers
            client.DefaultRequestHeaders.Add(Settings.RequestConstants.AuthorizationHeaderTitle, $"Bearer {request.AccessToken}");

            //Create body
            string data = JsonSerializer.Serialize(request.RequestData, typeof(GigaChatCompletionRequest));
            var response = await client.PostAsync(Settings.EndPoints.CompletionURL, new StringContent(data));

            CompletionResponse result = new CompletionResponse(response);
            LastResponse = result;

            //save to history
            if (LastResponse.RequestSuccessed)
            {
                var msg = LastResponse.GigaChatCompletionResponse.Choices.LastOrDefault()?.Message;
                if (useHistory && msg != null)
                    History.Add(msg);
            }

            return result;
        }
    }
}
