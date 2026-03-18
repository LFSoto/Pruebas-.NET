
using AutomationPracticeDemo.Test.Utils;

namespace AutomationPracticeDemo.Test.Tests.ContactUs.Assets;

public class MessageDataSource
{
    private const string nameJson = "DataMessage.json";

    /// <summary>
    /// Metodo que nos permite obtener la información del mensaje desde el archivo Json y nos permite separar los casos de prueba
    /// <returns></returns>
    public static IEnumerable<TestCaseData> MessageInformation()
    {
        var listaMessageInfo = JsonHelper.LoadListFromJson<MessageData>(nameJson);

        foreach (var item in listaMessageInfo)
        {
            yield return new TestCaseData(item);
        }
    }
}
