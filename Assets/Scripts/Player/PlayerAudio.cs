using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAudio : MonoBehaviour
{
    public AudioClip _jumpSound;
    public AudioClip _eatSound;
    public AudioClip _splitSound;

    private AudioPlayer _audioPlayer;

    private void Start()
    {
        _audioPlayer = AudioPlayer.Instance;
    }

    public void PlayJumpSound()
    {
        _audioPlayer.PlayEffect(_jumpSound);
    }

    public void PlayEatSound()
    {
        _audioPlayer.PlayEffect(_eatSound);
    }

    public void PlaySplitSound()
    {
        _audioPlayer.PlayEffect(_splitSound);
    }
}
