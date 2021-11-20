using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MissionNotiController : MonoBehaviour
{
    public GameObject missionClear, gameOver;
    

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null)
        {
            if (GameManager.Instance.isWin)
                missionClear.SetActive(true);
            else if (GameManager.Instance.isFail)
                gameOver.SetActive(true);
        }
    }
}