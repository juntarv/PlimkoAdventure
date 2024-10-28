using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class settingsButton : MonoBehaviour
{
    private SoundManager soundManager;
    public GameObject soundObject;
    public GameObject musicObject;

    private bool soundOn = true;
    private bool musicOn = true;
    public AudioClip audioClip;

    private SceneTransition sceneTransition;

    void Start()
    {
        soundManager = FindAnyObjectByType<SoundManager>();
        sceneTransition = FindObjectOfType<SceneTransition>();
        UpdateSoundDisplay();
        UpdateMusicDisplay();
    }

    public void ToggleSound()
    {
        soundOn = !soundOn;
        UpdateSoundDisplay();
        if (soundManager != null) // Check if reference is set
        {
            soundManager.OnMuteSoundChanged(!soundOn); // Call SoundManager method
        }
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
    }

    public void ToggleMusic()
    {
        musicOn = !musicOn;
        UpdateMusicDisplay();
        if (soundManager != null)
        {
            soundManager.OnMuteMusicChanged(!musicOn); // Call SoundManager method
        }
        if (audioClip != null)
        {
            // Проиграть аудио в центре экрана
            AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
        }
    }


    private void UpdateSoundDisplay()
    {
        soundObject.SetActive(soundOn);
    }

    private void UpdateMusicDisplay()
    {
        musicObject.SetActive(musicOn);
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

