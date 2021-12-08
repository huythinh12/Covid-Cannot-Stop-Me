using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CompletedMissionController : MonoBehaviour
{
    public Image start1, start2, start3,startNone1,startNone2,startNone3;

    private bool isTurnOn;

    // Update is called once per frame
    void Update()
    {
        if (GameManager.Instance != null && !isTurnOn)
        {
            isTurnOn = true;
            // do something 
            var heal = GameManager.Instance.countOfHeal;
            var death = GameManager.Instance.countOfDead;
            Result(death,heal);
        }
    }

    private void Result(int death, int heal)
    {
        if (death > 0)
        {
            if (heal >0 && heal<=5)
            {
                start1.gameObject.SetActive(true);
                startNone1.gameObject.SetActive(true);
                startNone2.gameObject.SetActive(true);
            }
            else if (heal > 5)
            {
                start1.gameObject.SetActive(true);
                start2.gameObject.SetActive(true);
                startNone1.gameObject.SetActive(true);
            }
            else
            {
                startNone1.gameObject.SetActive(true);
                startNone2.gameObject.SetActive(true);
                startNone3.gameObject.SetActive(true);
            }
        }
        else
        {
            if (heal >0 && heal<=5)
            {
                start1.gameObject.SetActive(true);
                start2.gameObject.SetActive(true);
                startNone2.gameObject.SetActive(true);
            }
            else if (heal > 5)
            {
                start1.gameObject.SetActive(true);
                start2.gameObject.SetActive(true);
                start3.gameObject.SetActive(true);
            }
            else
            {
                start1.gameObject.SetActive(true);
                startNone2.gameObject.SetActive(true);
                startNone3.gameObject.SetActive(true);
            }
        }
    }
}
