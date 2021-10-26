using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class PlayerAnimationController : MonoBehaviour
    {
        //Reference
        [SerializeField]
        private Animator playerAnimator;
        private int isIdleingHash;
        private int isWalkingHash;
        private int isRunningHash;
        private int isJumpingHash;
        private int isDrinkingHash;
        private int isThrowingHash;
        private int isHurtingHash;

        // Start is called before the first frame update
        void Start()
        {
            AnimatorHash();
        }

        // Update is called once per frame
        void Update()
        {
            PlayerMovementKey();
            PlayerDrinkKey();
            PlayerThrowKey();
        }
        // My method
        public void AnimatorHash()
        {
            playerAnimator = GetComponent<Animator>();
            isIdleingHash = Animator.StringToHash("IsIdle");
            isWalkingHash = Animator.StringToHash("IsWalk");
            isRunningHash = Animator.StringToHash("IsRun");
            isJumpingHash = Animator.StringToHash("IsJump");
            isDrinkingHash = Animator.StringToHash("IsDrink");
            isThrowingHash = Animator.StringToHash("IsThrow");
            isHurtingHash = Animator.StringToHash("IsHurt");


        }
        public void PlayerDrinkKey()
        {
            // get action input
            bool drinkPressed = Input.GetKey("c");
            bool isDrinking = playerAnimator.GetBool(isDrinkingHash);

            // set animation for action
            if(!isDrinking && drinkPressed) { playerAnimator.SetBool(isDrinkingHash, true); }
            else
            if (isDrinking && !drinkPressed) { playerAnimator.SetBool(isDrinkingHash, false); }

            //playerAnimator.ResetTrigger(isIdleingHash);
            //playerAnimator.SetTrigger(isDrinkingHash);

        }
        public void PlayerThrowKey()
        {
            // get action input
            bool throwPressed = Input.GetMouseButton(0);
            bool isThrowing = playerAnimator.GetBool(isThrowingHash);

            //set animation for action
            if(!isThrowing && throwPressed) { playerAnimator.SetBool(isThrowingHash, true); }
            else
            if(isThrowing && !throwPressed) { playerAnimator.SetBool(isThrowingHash, false); }

            //playerAnimator.ResetTrigger(isIdleingHash);
            //playerAnimator.SetTrigger(isThrowingHash);

        }
        public void PlayerIsHurt()
        {
            playerAnimator.SetBool(isHurtingHash, true);
        }

        private void PlayerMovementKey()
        {
            // get action input
            bool forwardPressed = Input.GetKey("w") || Input.GetKey("s") || Input.GetKey("a") || Input.GetKey("d");
            bool runPressed = Input.GetKey("left shift");
            bool jumpPressed = Input.GetKey("space");
            // set aniamtion for action
            playerAnimator.SetBool(isJumpingHash, jumpPressed);
            // running animation base on conditional
            if (forwardPressed && runPressed) { playerAnimator.SetBool(isRunningHash, true); }
            else
            if (!forwardPressed || !runPressed) { playerAnimator.SetBool(isRunningHash, false); }
        }
    }
}