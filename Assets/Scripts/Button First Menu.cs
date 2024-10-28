using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonFirstMenu : MonoBehaviour
{
    public AudioClip audioClip;
    private SceneTransition sceneTransition;

    void Start()
    {

        sceneTransition = FindObjectOfType<SceneTransition>();
    }
    public void Menu()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        if (sceneTransition != null)
        {
            sceneTransition.StartTransition("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }
    public void StartButton()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        if (sceneTransition != null)
        {
            sceneTransition.StartTransition("Choose Level");
        }
        else
        {
            SceneManager.LoadScene("Choose Level");
        }
    }
}
