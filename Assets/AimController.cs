using System;
using System.Collections;
using Player;
using UnityEngine;

namespace Player
{
    public class AimController : MonoBehaviour
    {
        [SerializeField] private Cinemachine.CinemachineVirtualCameraBase aimCamVirtual;
        
        public GameObject camThird, camAim;
        public GameObject aimRecticle;
        public static Vector3 aimDir;

        private BulletManager bulletManager;

        private void Start()
        {
            bulletManager = GetComponent<BulletManager>();
        }

        // Update is called once per frame
        void Update()
        {
            if (camAim.activeInHierarchy)
            {
                PlayerMovementController.isAiming = true;
                aimRecticle.SetActive(true);
                bulletManager.enabled = true;
                GetAimDir();
            }
            else
            {
                PlayerMovementController.isAiming = false;
                bulletManager.enabled = false;
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

        private void GetAimDir()
        {
            aimDir = aimCamVirtual.State.FinalOrientation * Vector3.forward;
        }
    }
}
