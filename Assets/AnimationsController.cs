using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationsController : MonoBehaviour
    {
        public float blendValue;
        private Animator anim;
        private int isIdleing;
        private int isWalking;
        private int isRunning;
        private int isJumping;
        private int isDrinking;
        private int isThrowing;
        private int isHurting;
        private int jump;
        private int death;
        private int attack;
        private int isFallEnough;
        private PlayerMovementController playerMovementController;

        // Start is called before the first frame update
        void Start()
        {
            playerMovementController = GetComponent<PlayerMovementController>();

            var characterObject = transform.GetChild(0);
            anim = characterObject.GetComponent<Animator>();
            isIdleing = Animator.StringToHash("isIdle");
            isWalking = Animator.StringToHash("isWalk");
            isRunning = Animator.StringToHash("isRun");
            isJumping = Animator.StringToHash("isJump");
            isDrinking = Animator.StringToHash("isDrink");
            isThrowing = Animator.StringToHash("isThrow");
            isHurting = Animator.StringToHash("isHurt");
            jump = Animator.StringToHash("jump");
            death = Animator.StringToHash("death");
            attack = Animator.StringToHash("attack");
            isFallEnough = Animator.StringToHash("isFallEnough");
        }

        // Update is called once per frame
        void Update()
        {
            ActionsInputKey();
        }

        private void ActionsInputKey()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;

            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool(isIdleing, false);
                if (Input.GetKey(KeyCode.LeftShift))
                {
                    anim.SetBool(isRunning, true);
                    anim.SetBool(isWalking, false);
                }
                else
                {
                    anim.SetBool(isWalking, true);
                    anim.SetBool(isRunning, false);
                }
            }
            else
            {
                anim.SetBool(isIdleing, true);
                anim.SetBool(isWalking, false);
                anim.SetBool(isRunning, false);
            }

            if (playerMovementController.IsGrounded)
            {
                anim.SetBool(isJumping,false);
                anim.SetBool(isFallEnough,true);
                
                if (Input.GetKeyDown(KeyCode.Space))
                {
                    anim.SetTrigger(jump);
                }
            }
            else
            {
                anim.SetBool(isFallEnough,false);
                anim.SetBool(isJumping,true);
            }

            if (Input.GetMouseButtonDown(0))
            {
                anim.SetTrigger(attack);
            }

            if (Input.GetKeyDown(KeyCode.C))
            {
            }
        }
    }
}