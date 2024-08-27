using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public Sound[] sound;

    void Awake()
    {

       foreach(Sound s in sound)
        {
            s.source = gameObject.AddComponent<AudioSource>();
            s.source.clip = s.clip;

            s.source.volume = s.volume;
            s.source.pitch = s.pitch;
            s.source.loop = s.loop;
        }
    }

    void Start()
    {
        Play("Lluvia");
        Play("Linea1");
        Play("SoundAmbient");
    }
    public void Play (string name)
    {
        Sound s= Array.Find (sound, sound => sound.name == name);

        if (s == null)
        {
            Debug.LogWarning("Sound: " + name + " not found!");
            return;
        }
            
        s.source.Play();
    }
}
