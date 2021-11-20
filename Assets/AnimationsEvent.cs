using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimationsEvent : MonoBehaviour
{
    //pending
    // public static bool isFallEnough;
    // public void TriggerFalling()
    // {
    //     isFallEnough = true;
    // }


    public void ThrowBall()
    {
        var ballScript = FindObjectOfType<Bullet>();
        ballScript.ReleaseMe();
    }
}