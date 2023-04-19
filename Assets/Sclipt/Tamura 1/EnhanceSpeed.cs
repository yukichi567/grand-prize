using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>スピードを強化するボタンにつけるスクリプト</summary>
public class EnhanceSpeed : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.SpeedUp(_stats[_count]);
        Debug.Log($"プレイヤーのスピードが{_stats[_count]}にあがりました。");
    }
}
