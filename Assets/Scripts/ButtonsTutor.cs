using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ButtonsTutor : MonoBehaviour
{

    public GameObject Tutor1;
    public GameObject Tutor2;
    public GameObject Tutor3;
    public AudioClip audioClip;



    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        Tutor1.SetActive(true);
        sceneTransition = FindObjectOfType<SceneTransition>();
    }


    public void Continue()
    {
        int tutor = PlayerPrefs.GetInt("tutor", 0);
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
        if (Tutor1.activeSelf)
        {
            Tutor1.SetActive(false);
            Tutor2.SetActive(true);
        }
        else if (Tutor2.activeSelf)
        {
            Tutor2.SetActive(false);
            Tutor3.SetActive(true);
        }
        else if (Tutor3.activeSelf)
        {
            PlayerPrefs.SetInt("tutor", 1);
            PlayerPrefs.Save();

            if (sceneTransition != null)
            {
                if (tutor == 0)
                {
                    PlayerPrefs.SetInt("tutor", 1);
                    PlayerPrefs.Save();
                    sceneTransition.StartTransition("Game");
                }
                PlayerPrefs.SetInt("tutor", 1);
                PlayerPrefs.Save();
                sceneTransition.StartTransition("Choose Level");
            }
            else
            {
                if (tutor == 0)
                {
                    PlayerPrefs.SetInt("tutor", 1);
                    PlayerPrefs.Save();
                    sceneTransition.StartTransition("Game");
                }
                PlayerPrefs.SetInt("tutor", 1);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Choose Level");
            }
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
            sceneTransition.StartTransition("Menu");
        }
        else
        {
            SceneManager.LoadScene("Menu");
        }
    }

   
}
