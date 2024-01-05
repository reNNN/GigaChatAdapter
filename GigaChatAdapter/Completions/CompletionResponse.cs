using System.Text.Json;

namespace GigaChatAdapter.Completions
{
    public class CompletionResponse
    {
        /// <summary>
        /// HTTP response
        /// </summary>
        public HttpResponseMessage HttpResponse { get; private set; }

        /// <summary>
        /// GigaChat response object from completion service
        /// </summary>
        public GigaChatCompletionResponse? GigaChatCompletionResponse { get; private set; }

        /// <summary>
        /// Indicates request status. Success [true] or not [false]
        /// </summary>
        public bool RequestSuccessed { get; private set; }

        /// <summary>
        /// Message if request authorization failed. Else empty.
        /// </summary>
        public string ErrorTextIfFailed { get; private set; }

        /// <summary>
        /// Response from completion service
        /// </summary>
        /// <param name="HttpMsg">Http response</param>
        public CompletionResponse(HttpResponseMessage HttpMsg)
        {
            HttpResponse = HttpMsg;
            string responseVal = HttpMsg.Content.ReadAsStringAsync().Result;

            //request successed
            if (HttpResponse.StatusCode == System.Net.HttpStatusCode.OK)
            {
                RequestSuccessed = true;
                GigaChatCompletionResponse = JsonSerializer.Deserialize<GigaChatCompletionResponse>(responseVal);
            }
            //request failed
            else
            {
                RequestSuccessed = false;
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
