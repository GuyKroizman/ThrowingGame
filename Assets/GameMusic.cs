using System.Collections.Generic;
using UnityEngine;

public class GameMusic : MonoBehaviour
{

    [SerializeField]
    public AudioSource[] musics;

    private int currentPlayingIndex = 0;
    private List<IPlayAudioSource> audioPlayers = new List<IPlayAudioSource>();

    interface IPlayAudioSource
    {
        void Play();
        void Stop();
    }

    class PlayAudioSource : IPlayAudioSource
    {
        private readonly AudioSource audioSource;

        public PlayAudioSource(AudioSource audioSource)
        {
            this.audioSource = audioSource;
        }

        public void Play()
        {
            this.audioSource.Play();
        }

        public void Stop()
        {
            this.audioSource.Stop();
        }
    }

    class DontPlayAudioSource : IPlayAudioSource
    {
        public void Play()
        { }

        public void Stop()
        { }
    }

    void Start()
    {
        musics[0].Play();

        for(int i = 0; i < musics.Length; i++)
        {
            audioPlayers.Add(new PlayAudioSource(musics[i]));
        }

        audioPlayers.Add(new DontPlayAudioSource());

    }

    public void PlayNextMusic()
    {
        audioPlayers[currentPlayingIndex].Stop();

        currentPlayingIndex++;
        if (currentPlayingIndex == audioPlayers.Count)
            currentPlayingIndex = 0;

        audioPlayers[currentPlayingIndex].Play();
        
    }
}
