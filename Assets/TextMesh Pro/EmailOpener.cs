using UnityEngine;
using UnityEngine.Networking;

public class EmailOpener : MonoBehaviour
{
    public void OpenEmailClient()
    {
        // Адрес получателя
        string recipient = "help@giahunginv.com";

        // Создаем ссылку mailto для открытия клиента электронной почты
        string mailtoUrl = $"mailto:{recipient}?subject={EscapeURL("Subject")}&body={EscapeURL("Subject")}";

        // Открытие ссылки mailto
        Application.OpenURL(mailtoUrl);
    }

    // Функция для корректной обработки специальных символов в URL
    private string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }
}
