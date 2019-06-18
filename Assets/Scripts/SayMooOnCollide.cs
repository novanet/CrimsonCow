using System.Linq;
using UnityEngine;

public class SayMooOnCollide : MonoBehaviour
{
    private AudioSource[] _audioSources;
    
    public void Start()
    {
        _audioSources = GetComponents<AudioSource>();
    }
    
    private void OnCollisionEnter(Collision other)
    {
        PlayRandomSound();
    }
    
    private void PlayRandomSound()
    {
        if (_audioSources.Length == 0)
            return;

        if (_audioSources.Any(x => x.isPlaying))
            return;
        
        int idx = Random.Range(0, _audioSources.Length); //min is inclusive, max is exclusive
        _audioSources[idx].Play();
        
    }
}
