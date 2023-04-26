using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP�ő�l")]protected int _maxHP;
    protected int _nowHP;
    [SerializeField, Header("�U����")]protected int _attackPower;

    private void Start()
    {
        _nowHP = _maxHP;
    }
    /// <summary>Enemy��HP�ő�l�𑝂₷�֐�</summary>
    /// <param name="upPoint">���₷�l</param>
    public void MaxHPUP(int upPoint)
    {
        _maxHP += upPoint;
    }

    /// <summary>Enemy�̍U���͂��グ��֐�</summary>
    /// <param name="powerUpPoint">���₷�l</param>
    public void AttackPowerUp(int powerUpPoint)
    {
        _attackPower += powerUpPoint;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            //Player��HP�[�U����
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
