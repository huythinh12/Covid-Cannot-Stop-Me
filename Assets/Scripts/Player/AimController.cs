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
                    camAim.SetActive(false);
                }
                else
                {
                    camThird.SetActive(false);
                    camAim.SetActive(!camAim.activeInHierarchy);
                }
            }
        }

        private void GetAimDir()
        {
            aimDir = aimCamVirtual.State.FinalOrientation * Vector3.forward;
        }
    }
}
