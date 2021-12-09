using System;
using UnityEngine.Audio;
using UnityEngine;
using TMPro;
using UnityEngine.Events;
using UnityEngine.UI;

namespace Menu
{
    public class AudioManager : MonoBehaviour
    {
        public static UnityEvent<float> OnVolumeChange = new UnityEvent<float>();
        public Text percentText;
        [SerializeField]
        private AudioSource mainMenuMusic;
        // [SerializeField]
        // private AudioMixer audioMixer;
        [SerializeField]
        private float musicVolume;
        public void Start()
        {
            // mainMenuMusic.Play();
        }
        public void Update()
        {
            mainMenuMusic.volume = musicVolume;
            percentText.text = Math.Round(musicVolume / 0.01f)  + "%";
        }
        public void UpdateVolume(float volume)
        {
            OnVolumeChange?.Invoke(volume);
            musicVolume = volume;
        }
    }
}