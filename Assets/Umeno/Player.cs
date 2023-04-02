using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    /// <summary>通常の移動スピード</summary>
    [SerializeField] float _defaltSpeed;
    /// <summary>ダッシュ時の移動スピード</summary>
    [SerializeField] float _dushSpeed;
    /// <summary>通常ジャンプの力</summary>
    [SerializeField] float _jumpPower;
    /// <summary>壁ジャンプの力</summary>
    [SerializeField] float _wallJumpPower;
    /// <summary>移動スピード</summary>
    [SerializeField] float _speed;
    [SerializeField] Vector2 _lineForWall = new Vector2(1f, 2f);
    [SerializeField] Vector2 _lineForRigth = Vector2.right;
    [SerializeField] Vector2 _lineForLeft = Vector2.left;
    [SerializeField] LayerMask _wallLayer = 0;
    [SerializeField] ParticleSystem _effect;
    [SerializeField] BoxCollider2D _boxCollider;
    bool _wallJump = false;
    bool _isGround = true;
    Rigidbody2D _rb;
    float _timer;
    int _intarval = 3;

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();

    }

    void Update()
    {
        _timer += Time.deltaTime;
        float x = Input.GetAxisRaw("Horizontal");
        Vector2 start = this.transform.position;
        Debug.DrawLine(start, start + _lineForWall);
        RaycastHit2D hit = Physics2D.Linecast(start, start + _lineForWall, _wallLayer);
        _rb.drag = 0;

        FlipX(x);
        if (_isGround)
        {
            PlayerMove(x);
            if (_timer >= _intarval)
            {
                _lineForRigth = new Vector2(1f, 2f);
                _timer = 0;
            }
        }

        if (Input.GetButtonDown("Fire3"))
        {
            StartCoroutine(Dush());
        }
        if (hit.collider)
        {
            _rb.drag = 10;
            Debug.Log("壁に当たった");
            if (Input.GetButtonDown("Jump"))
            {
                _wallJump = !_wallJump;
                if (_wallJump)
                {
                    _rb.velocity = new Vector2(1f, 2f).normalized * _wallJumpPower;
                    Debug.Log("右ジャンプ");
                    _lineForWall = new Vector2(1f, 2f);
                    FlipX(1f);
                }
                else
                {
                    _rb.velocity = new Vector2(-1f, 2f).normalized * _wallJumpPower;
                    Debug.Log("左ジャンプ");
                    _lineForWall = new Vector2(-1f, 2f);
                    FlipX(-1f);
                }
            }
        }

        //0.5秒間ダッシュする
        IEnumerator Dush()
        {
            Debug.Log("ダッシュ開始");
            _speed = _dushSpeed;
            yield return new WaitForSeconds(0.5f);
            Debug.Log("ダッシュ終了");
            _speed = _defaltSpeed;
        }
    }

    //プレイヤーの基本動作
    void PlayerMove(float X)
    {
        _rb.velocity = new Vector2(X * _speed, _rb.velocity.y);
        if (Input.GetButtonDown("Jump"))
        {
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }

    //プレイヤーの向きを変える
    void FlipX(float horizontal)
    {
        if (horizontal > 0)
        {
            transform.localScale = new Vector2(1, transform.localScale.y);
        }
        else if (horizontal < 0)
        {
            transform.localScale = new Vector2(-1, transform.localScale.y);
        }
    }
    //接地判定
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGround = true;
            Debug.Log("地面に着いた");
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.tag == "Ground")
        {
            _isGround = false;
            Debug.Log("地面から離れた");
        }
    }
}
