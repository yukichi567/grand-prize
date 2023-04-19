using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>ƒvƒŒƒCƒ„[‚Ì‹­‰»‚ğ•Û‘¶‚µ‚Ä‚é</summary>
public static class PlayerEnhance
{
    private static float _power = 0;
    public static float Power => _power;
    private static float _speed = 0;
    public static float Speed => _speed;
    private static float _chargeSpeed = 0;
    public static float ChargeSpeed => _chargeSpeed;

    public static void PowerUp(float power)
    {
        _power = power;
    }

    public  static void SpeedUp(float speed)
    {
        _speed = speed;
    }

    public static void ChargeSpeedUp(float chargeSpeed)
    {
        _chargeSpeed = chargeSpeed;
    }

}