using UnityEngine;

/// <summary>�v���C���[�̃X�s�[�h����������</summary>
public class PlayerSpeedGrow : PlayerGrowBase
{
    private static float _speed = 0;
    public static float Speed => _speed;

    protected override void Grow()
    {
        _speed = _stats[_count.Value];
        Debug.Log($"�X�s�[�h��{_speed}�ɂȂ�܂����B");
    }

}