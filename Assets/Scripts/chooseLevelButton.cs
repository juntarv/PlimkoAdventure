using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class chooseLevelButton : MonoBehaviour
{
    public AudioClip audioClip;
    public GameObject l1;
    public GameObject l2;
    private SceneTransition sceneTransition;
    // Start is called before the first frame update
    void Start()
    {
        
        sceneTransition = FindObjectOfType<SceneTransition>();
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
    public void L1()
    {
        l1.SetActive(true);
        l2.SetActive(false);
    }
    public void L2()
    {
        l1.SetActive(false);
        l2.SetActive(true);
    }
}
