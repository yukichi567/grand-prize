using UnityEngine;

/// <summary>�v���C���[�̃p���[����������</summary>
public class PlayerPowerGrow : PlayerGrowBase, IPlayerGrow
{
    private static float _power = 0;
    public static float Power => _power;

    protected override void Grow()
    {
        _power = _stats[_count.Value];
        Debug.Log($"�p���[��{_power}�ɂȂ�܂����B");
    }

}