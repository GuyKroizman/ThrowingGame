using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu(menuName ="Audio Events/Simple")]
public class SimpleAudioEvent : AudioEvent
{
    public AudioClip[] clips;
    public RangedFloat volume;

    [MinMaxRange(0, 2)]
    public RangedFloat pitch;

    public override void Play(AudioSource source)
    {    

        if (clips.Length == 0) return;

        int randomClip = Random.Range(0, clips.Length);
        source.clip = clips[randomClip];
        source.volume = Random.Range(volume.minValue, volume.maxValue);
        source.pitch = Random.Range(pitch.minValue, volume.maxValue);
        source.Play();
    }
}
