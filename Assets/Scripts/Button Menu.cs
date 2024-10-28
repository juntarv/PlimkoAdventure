using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonMenu : MonoBehaviour
{
    private SceneTransition sceneTransition;

    void Start()
    {

        sceneTransition = FindObjectOfType<SceneTransition>();
    }
    public string shareUrl;

    public AudioClip audioClip;
    public void Share()
    {
        new NativeShare()

            .SetUrl(shareUrl)
            .SetCallback((result, shareTarget) => Debug.Log("Share result: " + result + ", selected app: " + shareTarget))
            .Share();
    }

    public void Settings()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        if (sceneTransition != null)
        {
            sceneTransition.StartTransition("Settings");
        }
        else
        {
            SceneManager.LoadScene("Settings");
        }
    }
    public void Tutorial()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        if (sceneTransition != null)
        {
            sceneTransition.StartTransition("Tutorial");
        }
        else
        {
            SceneManager.LoadScene("Tutorial");
        }
    }
    public void Back()
    {
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }

        if (sceneTransition != null)
        {
            sceneTransition.StartTransition("First Menu");
        }
        else
        {
            SceneManager.LoadScene("First Menu");
        }
    }
}

