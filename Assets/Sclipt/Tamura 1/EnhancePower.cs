using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/// <summary>攻撃力を強化するボタンにつけるスクリプト</summary>
public class EnhancePower : EnhanceButtonBase
{
    protected override void Enhance()
    {
        PlayerEnhance.PowerUp(_stats[_count]);
        Debug.Log($"プレイヤーの攻撃力が{_stats[_count]}にあがりました。");
    }
}
