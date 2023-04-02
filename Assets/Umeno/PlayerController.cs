using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : InstanceSystem<PlayerController>
{
    [Header("プレイヤーの動きに関する数値")]
    [SerializeField, Tooltip("プレイヤーのスピード")] int _moveSpeed;
    [SerializeField, Tooltip("ジャンプのパワー")] float _jumpPower;
    [Header("設置判定に関する数値")]
    [SerializeField, Tooltip("設置判定のRayの長さ")] float _groundRayRange;
    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _groundLayer;
    Rigidbody2D _rb;
    float _x;
    float _y;
    bool _isGround;

    public bool IsGround { get => _isGround; set => _isGround = value; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        _x = Input.GetAxis("Horizontal");
        _y = Input.GetAxis("Vertical");
        _rb.velocity = new Vector2(_x, _rb.velocity.y) * _moveSpeed;
        if (_rb.velocity.magnitude > 0)
        {
            FlipX(_x);
        }

        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down * new Vector2(0, _groundRayRange), Mathf.Infinity, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * new Vector2(0, _groundRayRange), Color.red);
        if(hitGround)
        {
            IJudeSystem judgeSystem =  hitGround.collider.gameObject.GetComponent<IJudeSystem>();
            judgeSystem.GroundJudge();
        }
        if(_isGround && Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }


    void FlipX(float x)
    {
        if(x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y) ;
        }
        else if(x < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }
}