using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class EnemyOnTheGround : EnemyBase
{
    Rigidbody2D _rb;
    Animator _anim;
    SpriteRenderer _spriteRenderer;
    [SerializeField, Header("歩行速度")] float _walkSpeed;
    [SerializeField, Header("移動場所")] Transform[] _movePos;
    [SerializeField, Header("移動ありか")] bool _isMove;
    [SerializeField, Header("止まる動作があるかどうか")]bool _isStop;
    [SerializeField, Header("止まっている時間")] float _stopTime;
    float _stopTimer;
    int _movePosNumber = 0;
    float walkSpeed = 0;
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        _spriteRenderer = GetComponent<SpriteRenderer>();
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
            _stopTimer += Time.fixedDeltaTime;
            if (_stopTimer > 10 + _stopTime)
            {
                _isMove = true;
                _stopTimer = 0;
                
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
            _spriteRenderer.flipX = true;
        }
        else
        {
            _movePosNumber = 0;
            _spriteRenderer.flipX = false;
        }
    }
}
