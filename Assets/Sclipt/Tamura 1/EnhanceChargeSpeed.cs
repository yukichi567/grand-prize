using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>突進速度を強化するボタンにつけるスクリプト</summary>
public class EnhanceChargeSpeed : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.ChargeSpeedUp(_stats[_count]);
        Debug.Log($"プレイヤーの突進速度が{_stats[_count]}にあがりました。");
    }
}
