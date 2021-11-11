using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{
    [HideInInspector] public bool isJumpReady;
    [HideInInspector] public bool isFallEnough;
    public void TriggerJump()
    {
        isJumpReady = true;
    }

    public void TriggerFalling()
    {
        isFallEnough = false;
    }
}
