using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�X�s�[�h����������{�^���ɂ���X�N���v�g</summary>
public class EnhanceSpeed : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.SpeedUp(_stats[_count]);
        Debug.Log($"�v���C���[�̃X�s�[�h��{_stats[_count]}�ɂ�����܂����B");
    }
}
