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
        private int isCough;
        private int jump;
        private int death;
        private int drink;
        private int attack;
        private int layerIndexCough;
        private int layerIndexDeath;
        private bool isDeath;
        
        // private int isDrinking;
        // private int isThrowing;
        // private int isHurting;
        // private int isFallEnough;

        // Start is called before the first frame update
        void Start()
        {
            var characterObject = transform.GetChild(0);
            anim = characterObject.GetComponent<Animator>();
            GetAllAnimationStateFromAnimator();
            
            //get layerindexanimtor 
            layerIndexCough = anim.GetLayerIndex("Cough");
            layerIndexDeath = anim.GetLayerIndex("Death");
        }

        private void GetAllAnimationStateFromAnimator()
        {
            isIdleing = Animator.StringToHash("isIdle");
            isWalking = Animator.StringToHash("isWalk");
            isRunning = Animator.StringToHash("isRun");
            isJumping = Animator.StringToHash("isJump");
            isCough = Animator.StringToHash("isCough");
            jump = Animator.StringToHash("jump");
            death = Animator.StringToHash("death");
            drink = Animator.StringToHash("drink");
            attack = Animator.StringToHash("attack");
            
            // isFallEnough = Animator.StringToHash("isFallEnough");
            // isDrinking = Animator.StringToHash("isDrink");
            // isThrowing = Animator.StringToHash("isThrow");
            // isHurting = Animator.StringToHash("isHurt");
        }

        // Update is called once per frame
        void Update()
        {
            ActionsInputKey();
        }

        private void ActionsInputKey()
        {
            if (!CheckAnimtionDeath())
            {
                var isMove = GetDirection();
                //pending
                // CheckJumpAnimtion();
                CheckMoveToAnimation(isMove);
                CheckAttackAnimation();
                CheckHealingAnimation();
                // CheckCoughAnimation(isMove);
            }
         
        }

        private bool GetDirection()
        {
            float xAxis = Input.GetAxisRaw("Horizontal");
            float zAxis = Input.GetAxisRaw("Vertical");
            Vector3 direction = new Vector3(xAxis, 0f, zAxis).normalized;
            return direction.magnitude >= 0.1f;
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

        private void CheckMoveToAnimation(bool isMove)
        {
            if (isMove)
            {
                anim.SetBool(isIdleing, false);
                if (Input.GetKey(KeyCode.LeftShift) && !PlayerMovementController.isAiming /*&&
                    !AnimationsEvent.isFallEnough*/)
                {
                    //pending
                    // if (PlayerMovementController.hasKeyJump &&
                    //     PlayerMovementController.onGrounded) //check jump forward 
                    // {
                    //     PlayerMovementController.hasKeyJump = false;
                    //     anim.SetTrigger(jump);
                    //     anim.SetBool(isJumping, true);
                    // }
                    // else
                    // {
                        // anim.SetBool(isRunning, true);
                        // anim.SetBool(isWalking, false);
                    // }
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
        }

        //pending
        // private void CheckJumpAnimtion()
        // {
        //     // fall enough from in air animtion
        //     if (AnimationsEvent.isFallEnough && PlayerMovementController.onGrounded)
        //     {
        //         anim.SetBool(isJumping, false);
        //         AnimationsEvent.isFallEnough = false;
        //     }
        // }

        private void CheckCoughAnimation(bool isMove) {
            if (!PlayerMovementController.isAiming && !isMove)
            {
                anim.SetLayerWeight(layerIndexCough,1);
                anim.SetBool(isCough, true);
            }
            else 
            {
                anim.SetLayerWeight(layerIndexCough,0);
                anim.SetBool(isCough, false);
            }
        }

        private bool CheckAnimtionDeath()
        {
            //dead
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger(death);
                anim.SetLayerWeight(layerIndexDeath,1);
                return true;
            }//live
            else if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetLayerWeight(layerIndexDeath,0);
                return false;
            }
            return false;
        }
    }
}