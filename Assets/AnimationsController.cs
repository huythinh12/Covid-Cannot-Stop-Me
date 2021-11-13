using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Player
{
    public class AnimationsController : MonoBehaviour
    {
        private Animator anim;
        private int isIdleing;
        private int isWalking;
        private int isRunning;
        private int isJumping;
        // private int isDrinking;
        // private int isThrowing;
        // private int isHurting;
        private int jump;
        private int death;
        private int drink;
        private int attack;
        // private int isFallEnough;

        // Start is called before the first frame update
        void Start()
        {
            var characterObject = transform.GetChild(0);
            anim = characterObject.GetComponent<Animator>();
            GetAllAnimationStateFromAnimator();
        }

        private void GetAllAnimationStateFromAnimator()
        {
            isIdleing = Animator.StringToHash("isIdle");
            isWalking = Animator.StringToHash("isWalk");
            isRunning = Animator.StringToHash("isRun");
            isJumping = Animator.StringToHash("isJump");
            // isDrinking = Animator.StringToHash("isDrink");
            // isThrowing = Animator.StringToHash("isThrow");
            // isHurting = Animator.StringToHash("isHurt");
            jump = Animator.StringToHash("jump");
            death = Animator.StringToHash("death");
            drink = Animator.StringToHash("drink");
            attack = Animator.StringToHash("attack");
            // isFallEnough = Animator.StringToHash("isFallEnough");
        }

        // Update is called once per frame
        void Update()
        {
            ActionsInputKey();
        }

        private void ActionsInputKey()
        {
            var direction = GetDirection();
            CheckJumpAnimtion();
            CheckMoveToAnimation(direction);
            CheckAttackAnimation();
            CheckHealingAnimation();
        }

        private static Vector3 GetDirection()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;
            return direction;
        }

        private void CheckHealingAnimation()
        {
            if (Input.GetKeyDown(KeyCode.C))
            {
                anim.SetTrigger(drink);
            }
        }

        private void CheckAttackAnimation()
        {
            if (Input.GetMouseButtonDown(0) && PlayerMovementController.isAiming)
            {
                anim.SetTrigger(attack);
            }
        }

        private void CheckMoveToAnimation(Vector3 direction)
        {
            if (direction.magnitude >= 0.1f)
            {
                anim.SetBool(isIdleing, false);
                if (Input.GetKey(KeyCode.LeftShift) && !PlayerMovementController.isAiming && !AnimationsEvent.isFallEnough)
                {
                    if (PlayerMovementController.hasKeyJump && PlayerMovementController.onGrounded)
                    {
                        PlayerMovementController.hasKeyJump = false;
                        anim.SetTrigger(jump);
                        anim.SetBool(isJumping, true);
                    }
                    else
                    {
                        anim.SetBool(isRunning, true);
                        anim.SetBool(isWalking, false);
                    }
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
        }

        private void CheckJumpAnimtion()
        {
            if (AnimationsEvent.isFallEnough && PlayerMovementController.onGrounded)
            {
                anim.SetBool(isJumping, false);
                AnimationsEvent.isFallEnough = false;
            }
        }
    }
}