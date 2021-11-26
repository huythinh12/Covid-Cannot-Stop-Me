using UnityEngine;

public class Mission3Controller : MonoBehaviour
{
    public static int numberVirusDefeat;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
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