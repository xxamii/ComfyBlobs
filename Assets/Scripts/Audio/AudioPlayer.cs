using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

public class AudioPlayer : DontDestroyOnLoadSingleton<AudioPlayer>
{
    public AudioSource _effectsSource;
    public AudioSource _musicSource;
    public AudioSource _ambienceSource;
    public AudioMixer mixer;
    public string musicVolumeParameter;
    public string sfxVolumeParameter;

    private float _musicVolume;

    public void Start()
    {
        float musicVol = PlayerPrefs.GetFloat(musicVolumeParameter, 1);
        float sfxVol = PlayerPrefs.GetFloat(sfxVolumeParameter, 1);
        mixer.SetFloat(musicVolumeParameter, Mathf.Log10(musicVol) * 20);
        mixer.SetFloat(sfxVolumeParameter, Mathf.Log10(sfxVol) * 20);
    }

    public void PlayEffect(AudioClip clip, float volume = 1f)
    {
        _effectsSource.PlayOneShot(clip, volume);
    }

    public void PlayMusic(AudioClip clip)
    {
        if (_musicSource.clip == clip)
        {
            return;
        }

        _musicSource.Stop();
        _musicSource.clip = clip;
        _musicSource.Play();
    }

    public void PlayEndLevel()
    {
        _musicVolume = _musicSource.volume;
        _musicSource.volume = 0f;
        _ambienceSource.Play();
        StartCoroutine(ResumeMusicRoutine(4f));
    }

    private IEnumerator ResumeMusicRoutine(float t)
    {
        yield return new WaitForSeconds(t);
        _musicSource.volume = _musicVolume;
    }

    public AudioClip buttonHoverClip;
    public AudioClip buttonClickClip;
}
