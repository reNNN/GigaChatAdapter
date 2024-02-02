namespace GigaChatAdapter
{
    public class CompletionSettings
    {
        /// <summary>
        /// Gigachat version using. Default value 'GigaChat:latest'
        /// </summary>
        public string Model { get; set; }


        private float? _temperature;
        /// <summary>
        /// No required. Defaule by AI.
        /// Answer random range. From 0 to 2. See https://developers.sber.ru/docs/ru/gigachat/api/reference for details.
        /// !!! Not recommended to use together with TopP parameter. Set null one of them
        /// </summary>
        public float? Temperature
        {
            get => _temperature;
            set => _temperature = value == null ? null : value < 0 ? 0 : value > 2 ? 2 : value;//set range from 0 to 2. Set NULL if value is null. Set '0' if value is less than 0. Set '2' if more than 2
        }

        private float? _topP;
        /// <summary>
        /// No required. Defaule by AI.
        /// Range of most correct answer. From 0 to 1. See https://developers.sber.ru/docs/ru/gigachat/api/reference for details.
        /// !!! Not recommended to use together with Temperature parameter. Set null one of them
        /// </summary>
        public float? TopP
        {
            get => _topP;
            set => _topP = value == null ? null : value < 0 ? 0 : value > 1 ? 1 : value; //set range from 0 to 1. Set Null if value is null. Set '0' if value is less than 0. Set '1' if more than 1
        }

        private long? _count;
        /// <summary>
        /// No required. Defaule by AI.
        /// Response message count. From 1 to 4.
        /// </summary>
        public long? Count
        {
            get => _count;
            set => _count = value == null ? null : value < 1 ? 1 : value > 4 ? 4 : value; //set range from 1 to 4. Set Null if value is null. Set '1' if value is less than 1. Set '4' if more than 4
        }

        private long? _maxTokens;
        /// <summary>
        /// Maximum number of tokens that will be used to generate responses.
        /// </summary>
        public long? MaxTokens 
        {
            get => _maxTokens;
            set => _maxTokens = value == null ? null : value < 1 ? 1 : value; //  Set Null if value is null. Set '1' if value is less than 1. 
        }

        /// <summary>
        /// Create instance of request settings
        /// </summary>
        /// <param name="modelName">Name of uses model</param>
        /// <param name="temperature">Answer random range</param>
        /// <param name="topP">Range of most correct answer</param>
        /// <param name="count">Response message count</param>
        /// <param name="maxTokens">Response message length</param>
        public CompletionSettings(string modelName, float? temperature, float? topP, long? count, long? maxTokens)
        {
            Model = modelName;
            Temperature = temperature;
            TopP = topP;
            Count = count;
            MaxTokens = maxTokens;
        }
    }
}
