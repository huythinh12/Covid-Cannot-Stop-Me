using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission4Controller : MonoBehaviour
{
    public static bool isBossDead;

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
