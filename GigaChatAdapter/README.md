<h2>GigaChatAdapter .NET Core</h2>


**Capabilities**

➕ Easy to start to use GigaChat API. Only main classes and methods in root namespace. All helpers stored in child namespaces if you need set more details

➕ You can set all settings according official documentation https://developers.sber.ru/docs/ru/gigachat/api/reference

➕ The history can be saved (in file for example) and can be used in other sessions. Just load history before request prompt
  ```cs-sharp
  Completion completion = new Completion();
  completion.History = {Your deserialized history}
  ```

➕ All errors wrapped in one request field **'ErrorTextIfFailed'**. It includes HttpErrors and GigaChat errors

➕ Dont worry about access token expiring. Just use method **'UpdateToken()'** before sending request prompt

-------------------------
<h2>Example</h2>

The code example you can find below or in application "TestGigaChatAdapter"

**Step 1: //Set auth / Укажите аутентификационные данные из личного кабинета**

Below after code description how to get this authData in **Important!** tips
```cs-sharp
string authData = "authData=="; // base64
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
<h2>Important!</h2>

Before using you should execute 3 steps:
1) Registred in https://developers.sber.ru/ to get authenticated key that used for authorization. Generate auth code in personal account using button "Generate new Secret Code"
   
   ![image](https://github.com/reNNN/GigaChatAdapter/assets/8058272/1838fde6-ebee-4e13-85ac-56dc30365786)

2) Install certificates in OS. Information here: https://developers.sber.ru/docs/ru/gigachat/certificates
3) Access token lives only 30 mins. So use method UpdateToken() before sending request prompt
```cs-sharp
   auth.UpdateToken();
```
------------------------------
It is the first version of dll that i create for self using. Then it will be updated as needed. Welcome if you can help to upgrade this library :)
