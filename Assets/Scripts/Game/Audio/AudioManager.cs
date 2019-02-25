using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager :MonoBehaviour {
    public AudioSource audioSourcePrefab;
    private AudioClip[] _audios;

    private void Awake() {
        _audios = Resources.LoadAll<AudioClip>("Audios/");
    }

    public void Play(string name, float pitch = 1f, float duration = -1f) {
        var audioSource = Instantiate(audioSourcePrefab);
        audioSource.clip = Array.Find(_audios, x => x.name.Equals(name));
        audioSource.pitch = pitch;
        audioSource.Play();
        if (duration == -1f)
            Destroy(audioSource.gameObject, audioSource.clip.length);
        else
            Destroy(audioSource.gameObject, duration);
    }

    public void OnButtonClick() {
        Play("click");
    }

    public void OnFoldButtonClick() {
        Play("click2");
    }
}