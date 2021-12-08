using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudioController : MonoBehaviour
{
    [SerializeField] private AudioSource playerWalkSound;

    // Update is called once per frame
    void Update()
    {
        PlayerPlaySoundWhenInput();
    }

    private void PlayerPlaySoundWhenInput()
    {
        if (Input.GetKey(KeyCode.W)|| Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D) || Input.GetKey(KeyCode.S))
        {
            print("audio play");
            playerWalkSound.Play();
        }
        else
        {
            playerWalkSound.Stop();
        }
    }
}
