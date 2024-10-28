using UnityEngine;

public class StarsManager : MonoBehaviour
{
    public static StarsManager instance;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public int GetStarsForLevel(int level)
    {
        return PlayerPrefs.GetInt("Stars_Level_" + level, 0);
    }

    public void SetStarsForLevel(int level, int numStars)
    {
        PlayerPrefs.SetInt("Stars_Level_" + level, numStars);
        PlayerPrefs.Save();
    }
}

