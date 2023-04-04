using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sounds;
    // Start is called before the first frame update
    void Awake()
    {
        foreach(Sound s in sounds) {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;
            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    public void Play(string name, float lower, float higher) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if(s == null) return;
        print("Playing sound");
        float p = NextFloat(lower, higher);
        s.source.pitch = p;
        s.source.Play();
    }

    public void Play(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if(s == null) return;
        s.source.Play();
    }

    // public void Play(string name, float speed) {
    //     Sound s = Array.Find(sounds, sounds => sounds.name == name);
    //     if(s == null) return;
    //     s.source.pitch = speed;
    //     s.source.outputAudioMixerGroup.audioMixer.SetFloat("Pitch", 1f / speed);
    //     s.source.Play();

    // }

    public void Pause(string name) {
        Sound s = Array.Find(sounds, sounds => sounds.name == name);
        if(s == null) return;
        if(s.source.isPlaying) {
            s.source.Pause();
        }
    }

    static float NextFloat(float min, float max)
    {
        System.Random random = new System.Random();
        double val = (random.NextDouble() * (max - min) + min);
        return (float)val;
    }
}
