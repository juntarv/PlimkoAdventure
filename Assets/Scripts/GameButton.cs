using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class GameButton : MonoBehaviour
{

    private SceneTransition sceneTransition;

    void Start()
    {

        sceneTransition = FindObjectOfType<SceneTransition>();
    }
    public AudioClip audioClip;
    public void Back()
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
    public void Menu()
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
    public void Restart()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
