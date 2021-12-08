using System;
using UnityEngine;
using TMPro;

public class Mission3Controller : MonoBehaviour
{
    public static int numberVirusDefeat;

    private void Start()
    {
        numberVirusDefeat = 0;
        var playerNameUI = GameObject.FindWithTag("PlayerNameUI").GetComponent<TMP_Text>();
        playerNameUI.text = PlayerPrefs.GetString(DataPersistance.PLAYERNAME);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            // GameManager.Instance.isWin = false;
            if (PlayerHealth.isEmptyHP)
            {
                GameManager.Instance.isEndTime = true;
                GameManager.Instance.isFail = true;
            }
            else if (GameManager.Instance.isEndTime && numberVirusDefeat < 5)
            {
                GameManager.Instance.isFail = true;
                GameManager.Instance.isEndTime = true;
            }//win
            else if (numberVirusDefeat >= 5)
            {
                GameManager.Instance.isWin = true;
            }
        }
    }
}