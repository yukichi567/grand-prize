using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�ːi���x����������{�^���ɂ���X�N���v�g</summary>
public class EnhanceChargeSpeed : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.ChargeSpeedUp(_stats[_count]);
        Debug.Log($"�v���C���[�̓ːi���x��{_stats[_count]}�ɂ�����܂����B");
    }
}
