using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting.FullSerializer;
using UnityEngine;
using Random = UnityEngine.Random;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance;

    [Header("Audio sources references")]
    public AudioSourceConfig MainAmbiance_AudioSource;
    public AudioSourceConfig DialogAmbiance_AudioSource;
    public AudioSourceConfig MiniGame_AudioSource;
    public AudioSourceConfig SFX_AudioSource;

    [Header("Audio clips references")]
    public List<AudioClip> PositiveDialogClips;
    public List<AudioClip> NegativeDialogClips;

    public AudioClip AmbianceClip;
    public AudioClip SecondaryAmbianceClip;
    public AudioClip MiniGameClip;

    [Header("Parameters")]
    public float FadeDuration = .5f;

    private void Awake()
    {
        if (Instance != null)
        {
            Destroy(Instance);
        }

        Instance = this;
    }

    public void PlayDialogClip(bool isPositive)
    {
        List<AudioClip> list = isPositive ? PositiveDialogClips : NegativeDialogClips;
        AudioClip clip = list[Random.Range(0, list.Count)];
        SFX_AudioSource.AudioSource.volume = 1;
        SFX_AudioSource.AudioSource.PlayOneShot(clip);
    }

    public void ResetAmbianceAudio()
    {
        FadeIn(MainAmbiance_AudioSource);
        FadeOut(DialogAmbiance_AudioSource);
        FadeOut(MiniGame_AudioSource);
    }

    public void StartDialogAmbianceAudio()
    {
        FadeIn(DialogAmbiance_AudioSource);
        FadeOut(MainAmbiance_AudioSource);
    }

    public void StartMiniGameAudio()
    {
        FadeIn(DialogAmbiance_AudioSource);
        FadeOut(MainAmbiance_AudioSource);
    }

    private void FadeIn(AudioSourceConfig config)
    {
        StartCoroutine(Fade(config.AudioSource, 0, config.MaxVolume));
    }

    private void FadeOut(AudioSourceConfig config)
    {
        StartCoroutine(Fade(config.AudioSource, 0, config.MaxVolume));
    }

    private IEnumerator Fade(AudioSource audioSource, float startingVolume, float targetVolume)
    {
        float timer = 0;
        while (timer < FadeDuration) 
        {
            audioSource.volume = Mathf.Lerp(startingVolume, targetVolume, timer/FadeDuration);
            timer += Time.deltaTime;
            yield return new WaitForEndOfFrame();
        }
        audioSource.volume = targetVolume;
    }
}

[Serializable]
public class AudioSourceConfig
{
    public AudioSource AudioSource;
    public float MaxVolume;
}
