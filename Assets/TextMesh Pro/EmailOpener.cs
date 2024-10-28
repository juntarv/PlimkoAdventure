using UnityEngine;
using UnityEngine.Networking;

public class EmailOpener : MonoBehaviour
{
    public void OpenEmailClient()
    {
        // ����� ����������
        string recipient = "help@giahunginv.com";

        // ������� ������ mailto ��� �������� ������� ����������� �����
        string mailtoUrl = $"mailto:{recipient}?subject={EscapeURL("Subject")}&body={EscapeURL("Subject")}";

        // �������� ������ mailto
        Application.OpenURL(mailtoUrl);
    }

    // ������� ��� ���������� ��������� ����������� �������� � URL
    private string EscapeURL(string url)
    {
        return UnityWebRequest.EscapeURL(url).Replace("+", "%20");
    }
}
