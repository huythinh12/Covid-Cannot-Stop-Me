using UnityEngine;

public class DataPersistance : MonoBehaviour
{
    public const string PLAYERNAME = "PlayerName";
    public const string DIFFICULTY = "Difficulty";
    public const string VOLUME = "Volume";
    public static DataPersistance Instance;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    public void SavePlayerName(string name)
    {
        PlayerPrefs.SetString(PLAYERNAME,name);
    }

    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt(DIFFICULTY,difficulty);
    }
    
}
