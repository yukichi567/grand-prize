using UnityEngine;

/// <summary>プレイヤーのスピードを強化する</summary>
public class PlayerSpeedGrow : PlayerGrowBase
{
    private static float _speed = 0;
    public static float Speed => _speed;

    protected override void Grow()
    {
        _speed = _stats[_count];
        Debug.Log($"スピードが{_speed}になりました。");
    }

}