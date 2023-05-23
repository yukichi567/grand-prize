using UnityEngine;

/// <summary>プレイヤーのスピードを強化する</summary>
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
        PlayerData data = Resources.Load<PlayerData>("PlayerData");
        data.MaxSpeed.Value += 10;
        _speedGrowCount++;
        _speed = _stats[_speedGrowCount];
        Debug.Log($"スピードが{_speed}になりました。");
    }

}