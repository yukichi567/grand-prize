using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyInTheAir : EnemyBase
{
    [SerializeField, Header("�󒆂̓G�̓���")] EnemyType _enemyType;
    [SerializeField,Header("�㉺�ړ��̃X�s�[�h")]float _verticalMoveSpeed;
    [SerializeField, Header("���ړ��̃X�s�[�h")] float _horizonMoveSpeed;
    [SerializeField,Header("�㉺�ړ��͈̔�")]float _verticalMoveRange;
    [SerializeField, Header("���E�ړ��͈̔�")] float _horizonMoveRange;
    /// <summary>���̐܂�Ԃ��n�_</summary>
    Transform _turnPos;
    float time;
    int direction;
    int _movePosNumber = 0;
    Vector3 _StartPosition;
    float _ChangePosSaveY;
    float _ChangePosSaveX;
    Rigidbody2D _rb2;
    // Start is called before the first frame update
    void Start()
    {
        _StartPosition = transform.position;
        _rb2 = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(_enemyType.HasFlag(EnemyType.UpDown))
        {
            float sin = Mathf.Sin(time * _verticalMoveSpeed);
            //transform.position�œ��������ꍇ
            transform.position = _StartPosition + new Vector3(_ChangePosSaveX, sin * _verticalMoveRange, 0); ;
            _ChangePosSaveY = sin * _verticalMoveRange;
            //_rb2.velocity = new Vector3(0, (sin < 0 ? -1 : 1) * _verticalMoveRange, 0);
        }
        else
        {
            _ChangePosSaveY = 0;
        }

        if(_enemyType.HasFlag(EnemyType.LateralMove))
        {
            float sin = Mathf.Sin(Time.time * _horizonMoveSpeed);
            //transform.position�œ��������ꍇ
            transform.position = _StartPosition + new Vector3(sin * _horizonMoveRange, _ChangePosSaveY, 0);
            _ChangePosSaveX = sin * _horizonMoveRange;
            //_rb2.velocity = new Vector3((sin < 0 ? -1 : 1), 0, 0);
        }
        else
        {
            _ChangePosSaveX = 0;
        }

    }

    [Flags]
    enum EnemyType
    {
        /// <summary>�㉺�ɓ���</summary>
        UpDown = 1 << 0,
        /// <summary>���ړ�����</summary>
        LateralMove = 1 << 1,
    }
}
