using UnityEngine;
using UnityEngine.SceneManagement;

public class Levels : MonoBehaviour
{
    public int levelIndex; // ������ ������, ������� ���������� ��� ������
    public AudioClip audioClip;
    public GameObject levelComplette;
    public GameObject levelNotComplette;

    public GameObject[] stars; // ������ �������� ����

    private SceneTransition sceneTransition;

    private void Start()
    {
        // �������� ������� ������� ������
        int currentLevel = PlayerPrefs.GetInt("Level", 0);
        sceneTransition = FindObjectOfType<SceneTransition>();
        // ���������� ������
        if (currentLevel >= levelIndex)
        {
            levelComplette.SetActive(true);

            // ���������� �����
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
                // ��������� ����� � ������ ������
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
                // ��������� ����� � ������ ������
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
