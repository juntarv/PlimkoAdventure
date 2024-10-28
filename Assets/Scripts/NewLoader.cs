using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Networking;
using OneSignalSDK;
using System.Text;

public class NewLoader : MonoBehaviour
{
    string theUrl = "https://plimkoadventure.info/plimko";
    float timeout = 5f; 
    private string externalId;
    private int notificationRequestCount;

    
    DateTime requestAllowedDate = new DateTime(2024, 10, 27); 

    private void Awake()
    {
        PlayerPrefs.SetString("policy", theUrl);
        GenerateExternalId();
    }

    async void Start()
    {
        InitializeOneSignal();

        // ���������, ������ �� ������������� ���� ��� ���������� ��������
        if (CheckRequestDate())
        {
            // ���������, ���� �� ����������� ������ � ��������� ����� View � ������
            string lastVisitedUrl = PlayerPrefs.GetString("lastVisitedUrl", "");
            //Debug.Log("Last visited URL: " + lastVisitedUrl);
            if (!string.IsNullOrEmpty(lastVisitedUrl))
            {
                //Debug.Log("Loading View scene with last visited URL");
                SceneManager.LoadScene("View");
            }
            else
            {
                //Debug.Log("No last visited URL found, sending request to theUrl");
                StartCoroutine(SendRequest());
            }
        }
        else
        {
            //Debug.Log("Requests are not allowed yet. Loading First Menu.");
            SceneManager.LoadScene("First Menu");
        }

        RequestNotificationPermission();
    }

    // ��������, ������ �� ������������� ���� ��� ���������� ��������
    private bool CheckRequestDate()
    {
        DateTime currentDate = DateTime.Now;
        if (currentDate >= requestAllowedDate)
        {
            //Debug.Log("Requests are allowed.");
            return true; // ������� ����� ������
        }
        else
        {
            //Debug.Log("Requests are not allowed yet. Current date: " + currentDate);
            return false; // ������� ��� ������ ������
        }
    }

    IEnumerator SendRequest()
    {
        using (UnityWebRequest request = UnityWebRequest.Get(theUrl))
        {
            request.timeout = (int)timeout; // ������������� ������� ��� �������
            yield return request.SendWebRequest();

            //Debug.Log("Request Result: " + request.result);
           // Debug.Log("Request URL: " + request.url);
           // Debug.Log("Request Error: " + request.error);
           // Debug.Log("HTTP Response Code: " + request.responseCode); // ������� ��� ������

            if (request.result == UnityWebRequest.Result.Success && request.isDone)
            {
                if (request.responseCode == 200) // ���� �������� �����
                {
                    //Debug.Log("Request successful, loading View");

                    // ��������� URL, ����� ��������� ��� ��� ��������� �������
                    PlayerPrefs.SetString("lastVisitedUrl", request.url);
                    PlayerPrefs.Save();

                    SceneManager.LoadScene("View");
                }
                else
                {
                    //Debug.Log("Unexpected response, loading First Menu");
                    SceneManager.LoadScene("First Menu");
                }
            }
            else
            {
                Debug.Log("Request failed or 404 error, handling failure");
                HandleRequestFailure(request);
            }
        }
    }

    private void InitializeOneSignal()
    {
        OneSignal.Default.Initialize("57bd59ba-6ccf-43f1-af58-5aed0df65b53");
    }

    private async void RequestNotificationPermission()
    {
        // �������� ���������� �������� ���������� �� �����������
        notificationRequestCount = PlayerPrefs.GetInt("notificationRequestCount", 0);

        // ����������� ���������� �� �����������, ���� ���������� �������� ������ ����
        if (notificationRequestCount < 2)
        {
            bool accepted = await OneSignal.Notifications.RequestPermissionAsync(true);
            HandleNotificationPermission(accepted);
        }
    }

    private void HandleNotificationPermission(bool accepted)
    {
        if (!accepted)
        {
            // ����������� ���������� �������� � ���������
            notificationRequestCount++;
            PlayerPrefs.SetInt("notificationRequestCount", notificationRequestCount);
            PlayerPrefs.Save();
        }
    }

    private void GenerateExternalId()
    {
        // ���������, ���� �� ��� ����������� externalId
        if (PlayerPrefs.HasKey("externalId"))
        {
            externalId = PlayerPrefs.GetString("externalId");
            //Debug.LogWarning("Using saved External ID: " + externalId);
        }
        else
        {
            // ���� ���, ���������� ����� � ���������
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
            StringBuilder result = new StringBuilder(10);
            System.Random random = new System.Random();

            for (int i = 0; i < 10; i++)
            {
                result.Append(chars[random.Next(chars.Length)]);
            }

            externalId = result.ToString();
            //Debug.LogWarning("Generated External ID: " + externalId);
            PlayerPrefs.SetString("externalId", externalId);
            PlayerPrefs.Save();
        }

        // ��������� External ID � OneSignal
        OneSignal.Login(externalId);
    }

    void HandleRequestFailure(UnityWebRequest request)
    {
        if (request.responseCode == 404)
        {
            //Debug.Log("404 Not Found, loading First Menu");
            SceneManager.LoadScene("First Menu");
        }
        else if (request.result == UnityWebRequest.Result.ConnectionError ||
                 request.result == UnityWebRequest.Result.ProtocolError ||
                 request.result == UnityWebRequest.Result.DataProcessingError)
        {
            //Debug.LogError("Network error: " + request.error);
            SceneManager.LoadScene("First Menu");
        }
        else if (request.result == UnityWebRequest.Result.InProgress)
        {
            //Debug.LogError("Request timed out.");
            SceneManager.LoadScene("First Menu");
        }
    }
}
