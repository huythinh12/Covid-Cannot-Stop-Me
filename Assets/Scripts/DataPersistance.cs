using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DefaultNamespace;
public class DataPersistance : MonoBehaviour
{
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

    public void SetDifficulty(int difficulty)
    {
        PlayerPrefs.SetInt("Difficulty",difficulty);
    }

    public void SetSound(float sound)
    {
        PlayerPrefs.SetFloat("Sound",sound);
    }
}
