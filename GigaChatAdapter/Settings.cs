namespace GigaChatAdapter
{
    public static class Settings
    {
        public static class RequestConstants
        {
            public const string AuthorizationHeaderTitle = "Authorization";
            public const string RequestIDHeaderTitle = "RqUID";
            public const string RateScope = "scope";
        }

        public static class EndPoints
        {
            public const string AuthorizationURL = "https://ngw.devices.sberbank.ru:9443/api/v2/oauth";
            public const string CompletionURL = "https://gigachat.devices.sberbank.ru/api/v1/chat/completions";

        }

    }
}
