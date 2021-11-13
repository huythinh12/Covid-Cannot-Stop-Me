using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{
    // public static bool isJumpPlaying;
    // public static bool isJumpBegin;
    // public static bool isThrowEnd;
    public static bool isFallEnough;
    // public void TriggerJump()
    // {
    //     isJumpPlaying = true;
    // }
    //
    // public void TriggerJumpBegin()
    // {
    //     isJumpBegin = true;
    // }
    public void TriggerFalling()
    {
        isFallEnough = true;
    }

    // public void TriggerThrowEnd()
    // {
    //     isThrowEnd = true;
    // }
}
