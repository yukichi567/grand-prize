using UnityEngine;

/// <summary>プレイヤーの突進速度を強化する</summary>
public class PlayerChargeSpeedGrow : PlayerGrowBase
{
    private static float _chargeSpeed = 0;
    public static float ChargeSpeed => _chargeSpeed;

    protected override void Grow()
    {
        _chargeSpeed = _stats[_count];
        Debug.Log($"突進スピードが{_chargeSpeed}になりました。");
    }

}
