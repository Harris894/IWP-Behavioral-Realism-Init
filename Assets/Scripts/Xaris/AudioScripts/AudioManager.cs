using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance;

    public List<Sounds> greetingSounds = new List<Sounds>();
    public List<Sounds> orderingSounds = new List<Sounds>();
    public List<Sounds> responseSounds = new List<Sounds>();
    public List<Sounds> smallTalkSounds = new List<Sounds>();
    public List<Sounds> otherSounds = new List<Sounds>();

    public List<Sounds> soundFX = new List<Sounds>();


    private void Awake()
    {
        if (instance==null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }


    }

    public void PlayFXSound(string name, AudioSource source)
    {
        Sounds sound = new Sounds();
        for (int i = 0; i < soundFX.Count; i++)
        {
            if (soundFX[i].name == name)
            {
                sound = soundFX[i];
                sound.source = source;
                sound.source.clip = sound.clip;
            }
        }
        sound.source.Play();
    }

    public void StopSoundFX(AudioSource source)
    {
        source.Stop();
    }
    

    public void PlayGreetingSound(string name, AudioSource source)
    {
        Sounds sound = new Sounds();
        for (int i = 0; i < greetingSounds.Count; i++)
        {
            if (greetingSounds[i].name==name)
            {
                sound = greetingSounds[i];
                sound.source = source;
                sound.source.clip = sound.clip;
                Debug.Log(sound.name);
            }
            else
            {
                sound = greetingSounds[Random.Range(0, greetingSounds.Count)];
                sound.source = source;
                sound.source.clip = sound.clip;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        sound.source.Play();
    }


    public void PlayOrderingSound(string name, AudioSource source)
    {
        Debug.Log("Ordering Sound called");
        Sounds sound = null;
        for (int i = 0; i < orderingSounds.Count; i++)
        {
            if (orderingSounds[i].name == name)
            {
                sound = orderingSounds[i];
                sound.source = source;
                sound.source.clip = sound.clip;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + " not found");
            return;
        }
        sound.source.Play();

    }

    public void PlayResponseSound(string name, AudioSource source)
    {
        Sounds sound = new Sounds();

        if (name == "Thanks")
        {
            sound = responseSounds[Random.Range(2, 5)];
            sound.source = source;
            sound.source.clip = sound.clip;
            Debug.Log("Correct Order sound ran");
        }
        else if (name == "Wrong order")
        {
            sound = responseSounds[0];
            sound.source = source;
            sound.source.clip = sound.clip;
            Debug.Log("Wrong Order sound ran");
        }
        
        //{
        //    for (int i = 0; i < responseSounds.Count; i++)
        //    {
        //        if (responseSounds[i].name == name)
        //        {
        //            sound = responseSounds[i];
        //            sound.source = source;
        //            sound.source.clip = sound.clip;
        //            Debug.Log(sound.name);
        //        }
        //        else
        //        {
        //            Debug.LogError("ResponseSound name: " + name + "was not found");
        //        }
        //    }
        //}
        
        
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        sound.source.Play();
    }

    public void PlaySmallTalkSound(string name, AudioSource source)
    {
        Sounds sound = new Sounds();
        for (int i = 0; i < smallTalkSounds.Count; i++)
        {
            if (smallTalkSounds[i].name == name)
            {
                sound = smallTalkSounds[i];
                sound.source = source;
                sound.source.clip = sound.clip;
                Debug.Log(sound.name);
                break;
            }
            //else
            //{
            //    sound = smallTalkSounds[Random.Range(0, smallTalkSounds.Count)];
            //    sound.source = source;
            //    sound.source.clip = sound.clip;
            //}
        }
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        sound.source.Play();
    }

    public void PlayOtherSound(string name, AudioSource source)
    {
        Sounds sound = new Sounds();
        for (int i = 0; i < otherSounds.Count; i++)
        {
            if (otherSounds[i].name == name)
            {
                sound = otherSounds[i];
                sound.source = source;
                sound.source.clip = sound.clip;
                Debug.Log(sound.name);
            }
            else
            {
                sound = otherSounds[Random.Range(0, otherSounds.Count)];
                sound.source = source;
                sound.source.clip = sound.clip;
            }
        }
        if (sound == null)
        {
            Debug.LogWarning("Sound: " + name + "not found");
            return;
        }
        sound.source.Play();
    }
}
