using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerStats : MonoBehaviour
{
    public int level = 1;
    private int lev=1;
    public int score = 0;
    public int lives = 3;
    public int[] starsPerLevel;
    public GameObject life1Image;
    public GameObject life2Image;
    public GameObject life3Image;

    public GameObject win1Image;
    public GameObject win2Image;
    public GameObject win3Image;

    public GameObject WinMenu;
    public GameObject LoseMenu;

    public TextMeshProUGUI scoreTextMesh;

    public TextMeshProUGUI scoreTextWin;
    public TextMeshProUGUI scoreTextLose;
    public AudioClip audioClipWin;
    public AudioClip audioClipLose;

    public GameObject Level1;
    public GameObject Level2;
    public GameObject Level3;
    public GameObject Level4;
    public GameObject Level5;
    public GameObject Level6;
    public GameObject Level7;
    public GameObject Level8;
    private void Start()
    {
        Level1.SetActive(false);

        Level2.SetActive(false);


        Level3.SetActive(false);

        Level4.SetActive(false);

        Level5.SetActive(false);

        Level6.SetActive(false);

        Level7.SetActive(false);

        Level8.SetActive(false);
        starsPerLevel = new int[NumberOfLevels]; // Создаем массив для хранения количества звезд на каждом уровне
        LoadPlayerPrefs();
        UpdateLifeImages();
        LoseMenu.SetActive(false);
        WinMenu.SetActive(false);
        level = PlayerPrefs.GetInt("Level", 0);

    }

    public int NumberOfLevels
    {
        get { return starsPerLevel.Length; }
    }

    public void IncrementScore(int value)
    {
        score += value;
        UpdateScoreText();
    }

    public void DecrementLife()
    {
        lives--;
        UpdateLifeImages();
        if (lives <= 0)
        {
            GameOver();
        }
    }

    void GameOver()
    {
        scoreTextLose.text = "Score: " + score;
        LoseMenu.SetActive(true);
        if (audioClipLose != null)
        {
            AudioSource.PlayClipAtPoint(audioClipLose, transform.position);
        }

    }

    public void SetLevel(int newLevel)
    {
        level = newLevel;
        SavePlayerPrefs();
    }

    public void SetStarsForLevel(int level, int numStars)
    {
        starsPerLevel[level - 1] = numStars; // Устанавливаем количество звезд для определенного уровня
        SavePlayerPrefs();
    }

    public int GetStarsForLevel(int level)
    {
        return starsPerLevel[level - 1]; // Получаем количество звезд для определенного уровня
    }

    void LoadPlayerPrefs()
    {
        lev = PlayerPrefs.GetInt("LoadLevel", 1);
        for (int i = 0; i < NumberOfLevels; i++)
        {
            starsPerLevel[i] = PlayerPrefs.GetInt("Stars_Level_" + (i + 1), 0);
        }
        if (lev == 1)
        {
            Level1.SetActive(true);
        }
        else if (lev == 2)
        {
            Level2.SetActive(true);
        }
        else if(lev == 3)
        {
            Level3.SetActive(true);
        }
        else if (lev == 4)
        {
            Level4.SetActive(true);
        }
        else if (lev == 5)
        {
            Level5.SetActive(true);
        }
        else if (lev == 6)
        {
            Level6.SetActive(true);
        }
        else if (lev == 7)
        {
            Level7.SetActive(true);
        }
        else if (lev == 8)
        {
            Level8.SetActive(true);
        }
    }

    public void SavePlayerPrefs()
    {
        // Сохранить текущий уровень
        PlayerPrefs.SetInt("Level", level);

        // Сохранить количество звёзд для каждого уровня
        for (int i = 0; i < NumberOfLevels; i++)
        {
            PlayerPrefs.SetInt("Stars_Level_" + (i + 1), starsPerLevel[i]);
        }

        // Сохранить количество жизней
        PlayerPrefs.SetInt("Lives", lives);

        // Сохранить изменения
        PlayerPrefs.Save();
    }
    public void WinLevel()
    {
        // Отключаем Rigidbody игрока до следующего свайпа
        Rigidbody2D playerRigidbody = gameObject.GetComponent<Rigidbody2D>();
        if (playerRigidbody != null)
        {
            playerRigidbody.velocity = Vector2.zero;
            playerRigidbody.angularVelocity = 0f;
            playerRigidbody.bodyType = RigidbodyType2D.Static;
        }
        // Отобразить счёт
        scoreTextWin.text = "Score: " + score;


        // Отобразить изображение, соответствующее количеству жизней
        if (win1Image != null)
            win1Image.SetActive(lives == 1);
        if (win2Image != null)
            win2Image.SetActive(lives == 2);
        if (win3Image != null)
            win3Image.SetActive(lives == 3);

        // Увеличить номер уровня

        if (level > lev)
        {

            lev++;
            print(lev + "if " + level);

            PlayerPrefs.SetInt("LoadLevel", lev);
            PlayerPrefs.Save();
        }
        else
        {
            
            lev++;
            level = lev;
            print(lev + "else " + level);
            PlayerPrefs.SetInt("LoadLevel", level);
            PlayerPrefs.SetInt("LoadLevel", lev);
            PlayerPrefs.Save();
        }

        // Сохранить прогресс
        SavePlayerPrefs();

        // Проиграть звук победы (опционально)
        if (audioClipWin != null)
        {
            AudioSource.PlayClipAtPoint(audioClipWin, transform.position);
        }
        // Отобразить меню победы
        WinMenu.SetActive(true);
        // Количество звёзд за уровень равно количеству жизней
        int stars = lives;
        for (int i = 0; i < stars; i++)
        {
            // Сохранить количество звёзд для текущего уровня
            PlayerPrefs.SetInt("Stars_Level_" + level, stars);
        }

    }
    void UpdateLifeImages()
    {
        if (life1Image != null)
            life1Image.SetActive(lives == 1);
        if (life2Image != null)
            life2Image.SetActive(lives == 2);
        if (life3Image != null)
            life3Image.SetActive(lives == 3);
    }
    void UpdateScoreText()
    {
        if (scoreTextMesh != null)
        {
            scoreTextMesh.text = "Score:\n" + score; 
        }
    }
}
