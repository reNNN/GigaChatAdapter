This library allows you to use GigaChat API from Sber easly.
---------------------
Load it from NuGet or save dll and use locally.

The code example you can find below or in application "TestGigaChatAdapter":

**Step 1: //Set auth / Укажите аутентификационные данные из личного кабинета**

```cs-sharp
string authData = "authData==";
Authorization auth = new Authorization(authData, GigaChatAdapter.Auth.RateScope.GIGACHAT_API_PERS);
var authResult = await auth.SendRequest();
```

**Step 2: //Send Prompts to get answer from AI / отправляйте вопросы чату, чтобы получить ответ от ИИ**
```cs-sharp
if (authResult.AuthorizationSuccess)
{
    Completion completion = new Completion();
    Console.WriteLine("EN: Type prompt or close console to end application");
    Console.WriteLine("РУ: Напишите запрос к модели. В ином случае закройте окно, если дальнейшую работу с чатботом необходимо прекратить.");
    
    while (true)
    {
        //read prompt / Чтение промпта с консоли
        var prompt = Console.ReadLine();

        //update access token if expired / Обновление токена, если он просрочился
        await auth.UpdateToken();

        //request / отправка промпта. Чтобы исключить историю переписки - необходимо в методе указать false в качестве последнего аргумента (по умолчанию UseHistory = true)
        var result = await completion.SendRequest(auth.LastResponse.GigaChatAuthorizationResponse?.AccessToken, prompt);

        if (result.RequestSuccessed)
        {
            Console.WriteLine(result.GigaChatCompletionResponse.Choices.LastOrDefault().Message.Content);
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
```
---------------------------------------
**Important!**

Before using you should execute 3 steps:
1) registred in https://developers.sber.ru/ to get authenticated key that used for authorization
2) install certificates in OS. Information here: https://developers.sber.ru/docs/ru/gigachat/certificates
3) Access token lives only 30 mins. So use method UpdateToken() before sending request prompt
```cs-sharp
   auth.UpdateToken();
```
------------------------------
It is the first version of dll that i create for self using. Then it will be updated as needed. Welcome if you can help to upgrade this library :)
