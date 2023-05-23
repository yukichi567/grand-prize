using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyInTheAir : EnemyBase
{
    [SerializeField, Header("空中の敵の動き")] EnemyType _enemyType;
    [SerializeField,Header("上下移動のスピード")]float _verticalMoveSpeed;
    [SerializeField, Header("横移動のスピード")] float _horizonMoveSpeed;
    [SerializeField,Header("上下移動の範囲")]float _verticalMoveRange;
    [SerializeField, Header("左右移動の範囲")] float _horizonMoveRange;
    /// <summary>次の折り返し地点</summary>
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
            //transform.positionで動かした場合
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
            //transform.positionで動かした場合
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
        /// <summary>上下に動く</summary>
        UpDown = 1 << 0,
        /// <summary>横移動あり</summary>
        LateralMove = 1 << 1,
    }
}
