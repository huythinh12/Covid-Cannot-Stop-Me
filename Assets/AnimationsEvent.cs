using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{
    public static bool isJumpReady;
    public static bool isJumpBegin;
    public static bool isThrowEnd;
    [HideInInspector] public bool isFallEnough;
    public void TriggerJump()
    {
        isJumpReady = true;
    }

    public void TriggerJumpBegin()
    {
        isJumpBegin = true;
    }
    public void TriggerFalling()
    {
        isFallEnough = false;
    }

    public void TriggerThrowEnd()
    {
        isThrowEnd = true;
    }
}
