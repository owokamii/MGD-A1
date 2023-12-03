using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    [SerializeField] private AudioMixerGroup musicMixer;
    [SerializeField] private AudioMixerGroup sfxMixer;

    public Audio[] music;
    public Audio[] sfx;

    public static AudioManager instance;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        foreach (Audio m in music)
        {
            m.source = gameObject.AddComponent<AudioSource>();
            m.source.clip = m.clip;

            m.source.volume = m.volume;
            m.source.pitch = m.pitch;
            m.source.loop = m.loop;

            m.source.outputAudioMixerGroup = musicMixer;
        }

        foreach (Audio s in sfx)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;

            s.source.outputAudioMixerGroup = sfxMixer;
        }
    }

    void Start()
    {
        PlayMusic("BGM");
    }

    public void PlayMusic(string name)
    {
        Audio m = Array.Find(music, sound => sound.name == name);
        if (m == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        m.source.Play();
    }

    public void PlaySFX(string name)
    {
        Audio s = Array.Find(sfx, sound => sound.name == name);
        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
        s.source.Play();
    }
    //FindObjectOfType<AudioManager>().PlayMusic("BGM");
    //FindObjectOfType<AudioManager>().PlaySFX("SFX");
}