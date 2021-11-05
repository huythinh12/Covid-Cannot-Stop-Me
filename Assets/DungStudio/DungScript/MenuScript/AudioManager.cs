using UnityEngine.Audio;
using UnityEngine;
using TMPro;
namespace Menu
{
    public class AudioManager : MonoBehaviour
    {
        [SerializeField]
        private AudioSource mainMenuMusic;
        [SerializeField]
        private AudioMixer audioMixer;
        [SerializeField]
        private float musicVolume;
        public void Start()
        {
            mainMenuMusic.Play();
        }
        public void Update()
        {
            mainMenuMusic.volume = musicVolume;
        }
        public void UpdateVolume(float volume)
        {
            musicVolume = volume;
        }
    }
}