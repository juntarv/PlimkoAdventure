using System.Collections;
using System.Collections.Generic;
using System.Net.Http;
using System.Threading.Tasks;
using UnityEngine;

public class WebkaS : MonoBehaviour
{
    public UniWebView viewshka;

    void Start()
    {
        string lastVisitedUrl = PlayerPrefs.GetString("lastVisitedUrl", "");
        if (!string.IsNullOrEmpty(lastVisitedUrl))
        {
            viewshka.Load(lastVisitedUrl);
        }
        else
        {
            string defaultUrl = PlayerPrefs.GetString("policy", "");
            viewshka.Load(defaultUrl);
        }
    }

    public void BackButton()
    {
        viewshka.GoBack();
    }
}


