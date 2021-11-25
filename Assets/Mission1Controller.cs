using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mission1Controller : MonoBehaviour
{


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
            if (GameManager.Instance.isEndTime && GameManager.Instance.listInfected.Count >= 5)
            {
                GameManager.Instance.isFail = true;
            }
        }
    }
}