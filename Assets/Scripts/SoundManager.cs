using UnityEngine;
using UnityEngine.Audio;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Музыка")]
    public AudioClip backgroundMusic;
    public AudioSource musicSource;

    [Header("Звуки")]
    public AudioClip[] soundEffects;
    public AudioSource[] soundEffectSources;




    private void Awake()
    {
        InvokeRepeating("CheckIfStillExists", 1.0f, 1.0f); // Check every second
        // Singleton pattern
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;

        // Не уничтожать объект между сценами
        DontDestroyOnLoad(gameObject);


    }
    public void CheckIfStillExists()
    {
        if (Instance != this)
        {
            Destroy(gameObject);
        }
    }
    private void Start()
    {
        // Начать играть фоновую музыку
        PlayMusic();
    }

    public void PlayMusic()
    {
        if (musicSource != null && backgroundMusic != null)
        {
            musicSource.clip = backgroundMusic;
            musicSource.Play();
        }
    }

    public void StopMusic()
    {
        if (musicSource != null)
        {
            musicSource.Stop();
        }
    }

    public void PlaySound(int soundIndex)
    {
        if (soundIndex >= 0 && soundIndex < soundEffects.Length && soundEffectSources.Length > soundIndex)
        {
            AudioSource source = soundEffectSources[soundIndex];
            if (source != null)
            {
                source.PlayOneShot(soundEffects[soundIndex]);
            }
        }
    }


    public void OnMuteSoundChanged(bool mute)
    {
        foreach (AudioSource source in soundEffectSources) // Mute sound effects
        {
            if (source != null)
            {
                source.mute = mute;
            }
        }
    }

    public void OnMuteMusicChanged(bool mute)
    {
        if (musicSource != null) // Mute music
        {
            musicSource.mute = mute;
        }
    }

}

