using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [Header("AudioSource")]
    [Tooltip("BGMを再生するAudioSource")]
    [SerializeField] AudioSource _audioBgm;
    [Tooltip("SEを再生するAudioSource")]
    [SerializeField] AudioSource _audioSe;

    [Space]

    [Header("AudioClip")]
    [Tooltip("BGM")]
    [SerializeField] List<BgmSoundData> _bgmSoundDatas;
    [Tooltip("SE")]
    [SerializeField] List<SeSoundData> _seSoundDatas;

    [SerializeField]
    float _masterVolume = 1;
    [SerializeField]
    float _bgmMasterVolume = 1;
    [SerializeField]
    float _seMasterVolume = 1;

    private void Awake()
    {
        base.Awake();
    }
    /// <summary>
    /// BGMを再生するようにする
    /// </summary>
    /// <param name="bgm">再生したいBGMのenum</param>
    public void PlayBGM(BgmSoundData.BGM bgm)
    {
        int index = (int)bgm;
        BgmSoundData data = _bgmSoundDatas[index];
        _audioBgm.clip = data._audioClip;

        //音量の調節
        _audioBgm.volume = data._volume * _bgmMasterVolume * _masterVolume;
        _audioBgm.Play();
    }

    /// <summary>
    /// SEを再生するようにする
    /// </summary>
    /// <param name="se">再生したいSEのenum</param>
    public void PlaySE(SeSoundData.SE se)
    {
        int index = (int)se;
        SeSoundData data = _seSoundDatas[index];

        //音量の調節
        _audioSe.volume = data.Volume * _seMasterVolume * _masterVolume;
        _audioSe.PlayOneShot(data.AudioClip);
    }


    //BGM
    /*
     タイトルシーンのBGM
    ゲームシーンのBGM(サイバー系のハイテンポBGMがよさそう)
     */
    [System.Serializable]
    public struct BgmSoundData
    {

        public enum BGM
        {
            /// <summary>タイトルBGM</summary>
            Title,
            /// <summary>GameBGM</summary>
            Game,
        }

        public BGM _bgm;
        public AudioClip _audioClip;
        [Range(0f, 1f)]
        public float _volume;
    }


    //SE
    /*
    足音
    ジャンプ
    ボタンした時
    攻撃
    敵を倒したとき
    ステージクリア時*/
    [System.Serializable]
    public struct SeSoundData
    {
        public enum SE
        {
            /// <summary>足音</summary>
            FootSteps,
            /// <summary>ジャンプ</summary>
            Jump,
            /// <summary>ボタン押したとき</summary>
            Button,
            /// <summary>攻撃</summary>
            Fire,
            /// <summary>敵を倒したとき</summary>
            EnemyDown,
            /// <summary>ステージクリア</summary>
            Clear,
        }

        public SE Se;
        public AudioClip AudioClip;
        [Range(0, 1)]
        public float Volume;
    }
}
