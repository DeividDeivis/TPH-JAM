using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    //Lista de los sonidos en el inspector a partir de la clase Sound creada en otro script
    public Sound[] musicSounds, sfxSounds;
    public AudioSource musicSource, sfxSource;

    //Static Instance para poder acceder al AudioManager desde cualquier lugar
    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    //Play de musica comienzo
    private void Start()
    {
        //PlayMusic("name");
       
    }

    //Metodo para darle play a la musica
    public void PlayMusic(string name) 
    { 
        //busca en la lista musicSounds y llama a la musica que quiero reproducir 
        //por el nombre
        Sound s = Array.Find(musicSounds, x => x.name == name);

        //si no hay musica con ese nombre
        if (s == null) 
        {
            Debug.Log("Sound Not Found");
        }
        //si encuentra
        else 
        {
            musicSource.clip=s.clip;
            musicSource.PlayOneShot(s.clip);
        
        }
    
    }
    

    //lo mismo para los SFX
    public void PlaySFX(string name) 
    {
        Sound s = Array.Find(sfxSounds, x => x.name == name);

        if (s == null)
        {
            Debug.Log("Sound Not Found");
        }
        else
        {
            sfxSource.PlayOneShot(s.clip);

        }
    }

    //Para botonos y slider de control de Volumen
    public void ToggleMusic()
    { 
        musicSource.mute= !musicSource.mute;
    }
    public void ToggleSFX()
    {
        sfxSource.mute = !sfxSource.mute; 
    }

    public void MusicVolumen(float volumen)
    {
        musicSource.volume = volumen;
    }
    public void SFXVolume(float volumen)
    {
        sfxSource.volume = volumen;
    }
}
