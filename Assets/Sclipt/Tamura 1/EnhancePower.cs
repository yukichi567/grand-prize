using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>�U���͂���������{�^���ɂ���X�N���v�g</summary>
public class EnhancePower : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.PowerUp(_stats[_count]);
        Debug.Log($"�v���C���[�̍U���͂�{_stats[_count]}�ɂ�����܂����B");
    }
}
