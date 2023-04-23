using UnityEngine;

/// <summary>プレイヤーのパワーを強化する</summary>
public class PlayerPowerGrow : PlayerGrowBase, IPlayerGrow
{
    private static float _power = 0;
    public static float Power => _power;

    protected override void Grow()
    {
        _power = _stats[_count.Value];
        Debug.Log($"パワーが{_power}になりました。");
    }

}