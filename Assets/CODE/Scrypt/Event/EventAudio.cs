using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EventAudio : EventSystem
{
    [SerializeField] private AudioSource _audioSource;
    [SerializeField] private AudioClip _clip;
    [SerializeField] private bool _isPlaySong;
    public override void StartEvenement()
    {
        if(_isPlaySong)
        {
            _audioSource.clip = _clip;
            _audioSource.Play();
        }
        else
        {
            _audioSource.Stop();
        }
        
    }
}
