using System;
using Menu;
using UnityEngine;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;
    public Sound[] sounds;
    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
        
        foreach (Sound s in sounds)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
            s.source.volume = s.voulume;
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        Play("StartMenu");
    }

    private void OnEnable()
    {
        AudioManager.OnVolumeChange.AddListener(RecievedVolumeEvent);
    }

    private void OnDisable()
    {
        AudioManager.OnVolumeChange.RemoveListener(RecievedVolumeEvent);

    }

    private void RecievedVolumeEvent(float volume)
    {
        foreach (Sound s in sounds)
        {
            s.source.volume = volume;
        }
    }
    
    public void Play(string name)
    {
        Sound s = Array.Find(sounds, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound " + name + "Not Found");
            return;
        }
        s.source.Play();
    }
}
