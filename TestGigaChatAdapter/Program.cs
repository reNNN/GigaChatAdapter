using GigaChatAdapter;
using System.Text;

//Set Russian language in console / Настройка для работы консоли с кириллицей
Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
Console.InputEncoding = Encoding.GetEncoding(1251);
Console.OutputEncoding = Encoding.GetEncoding(1251);

//Set auth / Укажите аутентификационные данные из личного кабинета
string authData = "";

//Auth method / Запуск авторизации в гигачате
Authorization auth = new Authorization(authData, GigaChatAdapter.Auth.RateScope.GIGACHAT_API_PERS);
var authResult = await auth.SendRequest();

if (authResult.AuthorizationSuccess)
{
    Completion completion = new Completion();
    //Console.WriteLine("Type prompt or close console to end application"); //EN
    Console.WriteLine("Напишите запрос к модели. В ином случае закройте окно, если дальнейшую работу с чатботом необходимо прекратить."); //RU
    
    while (true)
    {
        //read prompt / Чтение промпта с консоли
        var prompt = Console.ReadLine();

        //update access token if expired (reserveTime before expiring - 1 min)/ Обновление токена, если он просрочился (запас времени - 1 минута до просрочки)
        await auth.UpdateToken(reserveTime: new TimeSpan(0, 1, 0));

        //Set settings / установка доп.настроек
        CompletionSettings settings = new CompletionSettings("GigaChat:latest", 2, null, 4, 1);

        //request / отправка промпта
        var result = await completion.SendRequest(auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken, prompt, true, settings);

        if (result.RequestSuccessed)
        {
            foreach (var it in result.GigaChatCompletionResponse.Choices)
            {
                Console.WriteLine(it.Message.Content);
            }
        }
        else
        {
            Console.WriteLine(result.ErrorTextIfFailed);
        }
    }

    
}
else
{
    Console.WriteLine(authResult.ErrorTextIfFailed);
}