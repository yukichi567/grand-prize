using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioManager : SingletonMonoBehaviour<AudioManager>
{
    [Header("AudioSource")]
    [Tooltip("BGM���Đ�����AudioSource")]
    [SerializeField] AudioSource _audioBgm;
    [Tooltip("SE���Đ�����AudioSource")]
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
    /// BGM���Đ�����悤�ɂ���
    /// </summary>
    /// <param name="bgm">�Đ�������BGM��enum</param>
    public void PlayBGM(BgmSoundData.BGM bgm)
    {
        int index = (int)bgm;
        BgmSoundData data = _bgmSoundDatas[index];
        _audioBgm.clip = data._audioClip;

        //���ʂ̒���
        _audioBgm.volume = data._volume * _bgmMasterVolume * _masterVolume;
        _audioBgm.Play();
    }

    /// <summary>
    /// SE���Đ�����悤�ɂ���
    /// </summary>
    /// <param name="se">�Đ�������SE��enum</param>
    public void PlaySE(SeSoundData.SE se)
    {
        int index = (int)se;
        SeSoundData data = _seSoundDatas[index];

        //���ʂ̒���
        _audioSe.volume = data.Volume * _seMasterVolume * _masterVolume;
        _audioSe.PlayOneShot(data.AudioClip);
    }


    //BGM
    /*
     �^�C�g���V�[����BGM
    �Q�[���V�[����BGM(�T�C�o�[�n�̃n�C�e���|BGM���悳����)
     */
    [System.Serializable]
    public struct BgmSoundData
    {

        public enum BGM
        {
            /// <summary>�^�C�g��BGM</summary>
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
    ����
    �W�����v
    �{�^��������
    �U��
    �G��|�����Ƃ�
    �X�e�[�W�N���A��*/
    [System.Serializable]
    public struct SeSoundData
    {
        public enum SE
        {
            /// <summary>����</summary>
            FootSteps,
            /// <summary>�W�����v</summary>
            Jump,
            /// <summary>�{�^���������Ƃ�</summary>
            Button,
            /// <summary>�U��</summary>
            Fire,
            /// <summary>�G��|�����Ƃ�</summary>
            EnemyDown,
            /// <summary>�X�e�[�W�N���A</summary>
            Clear,
        }

        public SE Se;
        public AudioClip AudioClip;
        [Range(0, 1)]
        public float Volume;
    }
}
