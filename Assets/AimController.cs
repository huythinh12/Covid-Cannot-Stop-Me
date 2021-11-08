using System;
using System.Collections;
using UnityEngine;

public class AimController : MonoBehaviour
{
    public GameObject camThird, camAim;

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Z))
        {
            if (!camThird.activeInHierarchy)
            {
                camThird.SetActive(!camThird.activeInHierarchy);
                StartCoroutine(DelayToActive(3));
            }
            else
            {
                camAim.SetActive(!camAim.activeInHierarchy);
                StartCoroutine(DelayToActive(3));
            }
        }
    }

    IEnumerator DelayToActive(float second)
    {
        yield return new WaitForSeconds(second);
        camAim.transform.GetChild(1).gameObject.SetActive(camAim.activeInHierarchy);
    }
}