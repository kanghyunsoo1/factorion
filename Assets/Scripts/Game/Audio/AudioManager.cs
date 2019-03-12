using System;
using UnityEngine;

public class AudioManager :Manager {
    public AudioSource audioSourcePrefab;
    private AudioClip[] _audios;

    private void Awake() {
        _audios = Resources.LoadAll<AudioClip>("Audios/");
    }

    public void Play(string name) {
        var audioSource = Instantiate(audioSourcePrefab);
        audioSource.clip = Array.Find(_audios, x => x.name.Equals(name));
        audioSource.Play();
        Destroy(audioSource.gameObject, audioSource.clip.length + 0.5f);
    }

    public void OnButtonClick() {
        Play("click");
    }

    public void OnFoldButtonClick() {
        Play("click2");
    }
}