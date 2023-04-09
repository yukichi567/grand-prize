using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class AudioController : InstanceSystem<AudioController>
{
    [SerializeField] AudioClip[] _clips;
    AudioSource _audio;
    private void Awake()
    {
        _audio = GetComponent<AudioSource>();
        base.Awake();
    }

    public void SePlay(SelectAudio se)
    {
        int index = (int)se;
        _audio.PlayOneShot(_clips[index]);
    }

}
public enum SelectAudio
{
    Click,
    Jump,
    Dush,
    Attack,
}