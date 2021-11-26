using UnityEngine;

public class Mission2Controller : MonoBehaviour
{
    public static int numberVaccineInTown;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if(PlayerHealth.isEmptyHP)
            {
                GameManager.Instance.isEndTime = true;
                GameManager.Instance.isFail = true;
            }
            else if (GameManager.Instance.isEndTime && numberVaccineInTown < 5)
            {
                GameManager.Instance.isFail = true;
                GameManager.Instance.isEndTime = true;
            }
            else if (numberVaccineInTown >= 5)
            {
                print("win r ne");
                GameManager.Instance.isWin = true;
            }
        }
    }
}
