using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : MonoBehaviour
{
    public List<Music> music;
    public bool isPlayingMusic {get; private set;}
    private Music currentSong;

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

    public void PlayMusic(string clipName)
    {
        if (currentSong != null) StopMusic();
        currentSong = music.Find((song)=> song.clip.name == clipName);
        currentSong.source.Play();
    }

    public void PauseMusic()
    {
        currentSong.source.Pause();
    }

    public void ResumeMusic()
    {
        currentSong.source.UnPause();
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
        currentSong.source.Stop();
        currentSong = null;
    }
}
