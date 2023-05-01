using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Music> music;
    public bool isPlayingMusic {get; private set;}
    public Music currentSong;

    // Start is called before the first frame update
    void Start()
    {
        if (FindObjectsOfType<AudioManager>().Length > 1) Destroy(this);
        else DontDestroyOnLoad(this);

        foreach (Music song in music)
        {
            song.source = gameObject.AddComponent<AudioSource>();
            song.source.clip = song.clip;
            song.source.loop = true;
        }
        currentSong = null;
    }

    // void Update()
    // {
    //     Debug.Log(currentSong);
    // }

    public void PlayMusic(string clipName)
    {
        if (currentSong != (Music)null) StopMusic();
        currentSong = music.Find((song)=> song.clip.name == clipName);
        currentSong.source.volume = PlayerPrefs.GetFloat("Volume", 0.2f);
        currentSong.source.Play();
        isPlayingMusic = true;
    }

    public void PauseMusic()
    {
        currentSong.source.Pause();
        isPlayingMusic = false;
    }

    public void ResumeMusic()
    {
        currentSong.source.UnPause();
        isPlayingMusic = true;
    }

    public void ToggleMusic()
    {
        if (isPlayingMusic)
        {
            isPlayingMusic = false;
            PauseMusic();
        }
        else
        {
            isPlayingMusic = true;
            ResumeMusic();
        }
    }

    public void StopMusic()
    {
        if (isPlayingMusic)
        {
            isPlayingMusic = false;
            currentSong.source.Stop();
            currentSong = null;
        }
        
    }
}
