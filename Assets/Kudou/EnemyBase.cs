using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP最大値")] int _maxHP;
    [SerializeField, Header("攻撃力")] int _attackPower;
    
    /// <summary>EnemyのHP最大値を増やす関数</summary>
    /// <param name="upPoint">増やす値</param>
    public void MaxHPUP(int upPoint)
    {
        _maxHP += upPoint;
    }

    /// <summary>Enemyの攻撃力を上げる関数</summary>
    /// <param name="powerUpPoint">増やす値</param>
    public void AttackPowerUp(int powerUpPoint)
    {
        _attackPower += powerUpPoint;
    }
}
