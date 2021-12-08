using UnityEngine;
using TMPro;
public class Mission4Controller : MonoBehaviour
{
    public static bool isBossDead;

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
            else if (GameManager.Instance.isEndTime && !isBossDead)
            {
                GameManager.Instance.isFail = true;
                GameManager.Instance.isEndTime = true;
            }
            else if (isBossDead)
            {
                GameManager.Instance.isWin = true;
            }
        }
    }
}
