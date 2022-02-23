using UnityEngine;
using TMPro;

public class Mission1Controller : MonoBehaviour
{
    private void Start()
    {
        var playerNameUI = GameObject.FindWithTag("PlayerNameUI").GetComponent<TMP_Text>();
        playerNameUI.text = PlayerPrefs.GetString(DataPersistance.PLAYERNAME);
    }

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if(PlayerHealth.isEmptyHP )
            {
                GameManager.Instance.isEndTime = true;
                GameManager.Instance.isFail = true;
            }
            else if (GameManager.Instance.isEndTime && GameManager.Instance.listInfected.Count > 5 || GameManager.Instance.listInfected.Count > 5)
            {
                GameManager.Instance.isFail = true;
                GameManager.Instance.isEndTime = true;
            }
            else if (GameManager.Instance.isEndTime && GameManager.Instance.listInfected.Count <= 5)
            {
                GameManager.Instance.isWin = true;
                GameManager.Instance.isEndTime = true;
            }
        }
    }
}