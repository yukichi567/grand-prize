using UnityEngine;

/// <summary>�v���C���[�̃X�s�[�h����������</summary>
public class PlayerSpeedGrow : PlayerGrowBase
{
    private static float _speed = 0;
    public static float Speed => _speed;
    private static int _speedGrowCount = 0;

    private void Start()
    {
        _count.Value = _speedGrowCount;
    }


    protected override void Grow()
    {
        _speedGrowCount++;
        _speed = _stats[_speedGrowCount];
        Debug.Log($"�X�s�[�h��{_speed}�ɂȂ�܂����B");
    }

}