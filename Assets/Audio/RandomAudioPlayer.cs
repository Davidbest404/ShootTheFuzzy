using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomAudioPlayer : MonoBehaviour
{
    public List<AudioClip> audioClips = new List<AudioClip>();

    private AudioSource audioSource;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();

        PlayRandomTrack();
    }

    void PlayRandomTrack()
    {
        if (audioClips.Count > 0)
        {
            int randomIndex = Random.Range(0, audioClips.Count);

            audioSource.clip = audioClips[randomIndex];

            audioSource.Play();

            StartCoroutine(CheckForEndOfPlayback());
        }
        else
        {
            Debug.LogWarning("Нет аудиофайлов в списке!");
        }
    }

    IEnumerator CheckForEndOfPlayback()
    {
        while (audioSource.isPlaying)
        {
            yield return null;
        }

        PlayRandomTrack();
    }
}