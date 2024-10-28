using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public int levelIndex; // Индекс уровня, который отображает эта иконка
    public AudioClip audioClip;
    public GameObject levelComplette;
    public GameObject levelNotComplette;

    public GameObject[] stars; // Массив объектов звёзд

    private SceneTransition sceneTransition;

    private void Start()
    {
        // Получить текущий уровень игрока
        int currentLevel = PlayerPrefs.GetInt("Level", 0);
        sceneTransition = FindObjectOfType<SceneTransition>();
        // Отобразить иконку
        if (currentLevel >= levelIndex)
        {
            levelComplette.SetActive(true);

            // Отобразить звёзды
            int starsCount = PlayerPrefs.GetInt("Stars_Level_" + (levelIndex+1), 0);
            for (int i = 0; i < starsCount; i++)
            {
                stars[i].SetActive(true);
            }
        }
        else
        {
            levelNotComplette.SetActive(true);
            levelComplette.SetActive(false);
        }
    }
    public void StartLevel()
    {
        int currentLevel = PlayerPrefs.GetInt("Level", 1);
        int tutor = PlayerPrefs.GetInt("tutor", 0);
        if (tutor == 0)
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
        else
        {
            if (audioClip != null)
            {
                // Проиграть аудио в центре экрана
                AudioSource.PlayClipAtPoint(audioClip, Camera.main.transform.position);
            }
            if (currentLevel >= levelIndex)
            {
                PlayerPrefs.SetInt("LoadLevel", levelIndex);
                PlayerPrefs.Save();
                SceneManager.LoadScene("Game");
            }
        }
        
        
    }

}
