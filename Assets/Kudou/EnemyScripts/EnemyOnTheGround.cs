using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyOnTheGround : EnemyBase
{
    Rigidbody2D _rb;
    Animator _anim;
    [SerializeField, Header("���s���x")] float _walkSpeed;
    [SerializeField, Header("�ړ��ꏊ")] Transform[] _movePos;
    [SerializeField, Header("�ړ����肩")] bool _isMove;
    [SerializeField, Header("�~�܂铮�삪���邩�ǂ���")]bool _isStop;
    [SerializeField, Header("�~�܂��Ă��鎞��")] float _stopTime;
    float _stopTimer;
    int _movePosNumber = 0;
    float walkSpeed = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        walkSpeed = _walkSpeed;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if(_isMove)
        {
            Move();
        }

        if (_isStop)
        {
            _stopTimer += Time.deltaTime;
            if (_stopTimer > 10 + _stopTime)
            {
                _isMove = true;
                
            }
            else if (_stopTimer > 10)
            {
                _isMove = false;  
            }
        }
        _anim.SetBool("Move", _isMove);
    }

    private void Move()
    {
        Vector3 dir = _movePos[_movePosNumber].position - transform.position;
        if (dir.magnitude > 1)
        {
            _rb.velocity = new Vector3(dir.normalized.x * _walkSpeed * Time.deltaTime,0,0);
        }
        else
        {
            DestinationPosChange();
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
