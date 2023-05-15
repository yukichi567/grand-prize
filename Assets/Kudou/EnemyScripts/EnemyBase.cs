using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP最大値")]protected int _maxHP;
    protected int _nowHP;
    [SerializeField, Header("Playerが取得するポイント")] int _point;
    [SerializeField, Header("攻撃力")]protected int _attackPower;
    protected PlayerController _playerController;
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
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
            Attack();
        }
    }

    public void Attack()
    {
        _playerController.HP -= _attackPower;
    }
    public void  Damage(int damage)
    {
        _nowHP -= damage;
        if(_nowHP <= 0)
        {
            GameManager.Instance.PointPlus(_point);
            Destroy(gameObject);
        }
    }
}
