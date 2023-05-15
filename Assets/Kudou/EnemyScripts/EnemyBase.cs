using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP�ő�l")]protected int _maxHP;
    protected int _nowHP;
    [SerializeField, Header("Player���擾����|�C���g")] int _point;
    [SerializeField, Header("�U����")]protected int _attackPower;
    protected PlayerController _playerController;
    private void Start()
    {
        _playerController = FindObjectOfType<PlayerController>();
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
