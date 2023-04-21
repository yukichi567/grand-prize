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
    Vector3 _ChangePosSave;
    // Start is called before the first frame update
    void Start()
    {
         
        _StartPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        time += Time.deltaTime;
        if(_enemyType.HasFlag(EnemyType.UpDown))
        {
            float sin = Mathf.Sin(time * _verticalMoveSpeed);
            
            transform.position = _StartPosition + _ChangePosSave + new Vector3(0, sin * _verticalMoveRange, 0);;
            _ChangePosSave = new Vector3(0, sin * _verticalMoveRange, 0);
        }

        if(_enemyType.HasFlag(EnemyType.LateralMove))
        {
            float sin = Mathf.Sin(time * _horizonMoveSpeed);
            transform.position = _StartPosition + _ChangePosSave + new Vector3(sin * _horizonMoveRange, 0, 0);
            _ChangePosSave = new Vector3(sin * _horizonMoveRange, 0, 0);
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
