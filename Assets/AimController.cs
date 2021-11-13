using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Player
{
    public class AimController : MonoBehaviour
    {
        public GameObject camThird, camAim;
        public GameObject aimRecticle;

        
        // Update is called once per frame
        void Update()
        {
            if (camAim.activeInHierarchy)
            {
                PlayerMovementController.isAiming = true;
                aimRecticle.SetActive(true);
            }
            else
            {
                PlayerMovementController.isAiming = false;
                aimRecticle.SetActive(false);
            }
            if (Input.GetMouseButtonDown(1))
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
}
