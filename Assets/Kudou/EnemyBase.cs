using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP最大値")]protected int _maxHP;
    protected int _nowHP;
    [SerializeField, Header("攻撃力")]protected int _attackPower;

    private void Start()
    {
        _nowHP = _maxHP;
    }
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

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //PlayerのHPー攻撃力
        }
    }

    private void  Damage(int damage)
    {
        _nowHP -= damage;
        if(_nowHP < 0)
        {
            Destroy(gameObject);
        }
    }
}
