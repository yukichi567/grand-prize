using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [SerializeField, Header("HP�ő�l")] int _maxHP;
    [SerializeField, Header("�U����")] int _attackPower;
    
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
}
