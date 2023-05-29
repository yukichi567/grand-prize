using UnityEngine;

/// <summary>プレイヤーのパワーを強化する</summary>
public class PlayerPowerGrow : PlayerGrowBase, IPlayerGrow
{
    private static float _power = 0;
    public static float Power => _power;
    private static int _powerGrowCount = 0;

    private void Start()
    {
        _count.Value = _powerGrowCount;
    }

    protected override void Grow()
    {
        PlayerData data = Resources.Load<PlayerData>("PlayerData");
        _powerGrowCount++;
        _power = _stats[_powerGrowCount];
        data.MaxPower.Value = (int)_power;
        Debug.Log($"パワーが{_power}になりました。");
    }

}