using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyOnTheGround : EnemyBase
{
    Rigidbody2D _rb;
    [SerializeField, Header("���s���x")] float _walkSpeed;
    [SerializeField, Header("�ړ��ꏊ")] Transform[] _movePos;
    [SerializeField, Header("�ړ����肩")] bool _isMove;
    int _movePosNumber = 0;
    
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();    
    }

    // Update is called once per frame
    void Update()
    {
        if(_isMove)
        {
            Move();
        }
    }

    private void Move()
    {
        Vector3 dir = _movePos[_movePosNumber].position - transform.position;
        if (dir.magnitude > 1)
        {
            _rb.velocity = new Vector3(dir.normalized.x * _walkSpeed * Time.fixedDeltaTime,0,0);
            Debug.Log(dir.magnitude);
        }
        else
        {
            DestinationPosChange();
            Debug.Log(_movePosNumber);
        }
    }

    void DestinationPosChange()
    {
        if(_movePosNumber == 0)
        {
            _movePosNumber = 1;
        }
        else
        {
            _movePosNumber = 0;
        }
    }
}
