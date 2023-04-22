using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpTest : InstanceSystem<PlayerController>
{
    [Header("プレイヤーの動きに関する数値")]
    [SerializeField, Tooltip("通常時のスピード")] int _defaultSpeed;
    [SerializeField, Tooltip("ダッシュ時のスピード")] int _dushSpeed;
    [SerializeField, Tooltip("ジャンプのパワー")] float _jumpPower;
    [Header("設置判定のRayに関する数値")]
    [SerializeField, Tooltip("設置判定のRayの長さ")] float _groundRayRange;
    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _groundLayer;
    [Header("エネミーに関する数値")]
    [SerializeField, Tooltip("ロックオンのカーソル")] GameObject _cursor;
    [Header("プレイヤーアタックに関する数値")]
    [SerializeField, Tooltip("アタック用のオブジェクト")] GameObject _attackObject;
    Rigidbody2D _rb;
    Vector3 _enemyPosition;
    Animator _anim;
    float _x;
    bool _isGround;
    bool _isWallJump;
    bool _isEnemyRock;
    bool _isEnemyDush;

    public bool IsGround { get => _isGround; set => _isGround = value; }
    public bool IsWallJump { get => _isWallJump; set => _isWallJump = value; }

    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }

    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        _anim.SetFloat("Speed", _rb.velocity.magnitude);
        //接地判定
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        if (hitGround)
        {
            //床オブジェクトにあるJudge関数でboolを変更する(接地判定)
            _isGround = true;
        }
        else
        {
            _isGround = false;
        }

        if (Input.GetButtonDown("Jump"))
        {
            if (_isEnemyRock)
            {
                _rb.velocity = Vector3.zero;
                _isEnemyDush = true;
                var dir = (_enemyPosition - transform.position).normalized;
                _rb.AddForce(dir * _jumpPower, ForceMode2D.Impulse);
                _isEnemyDush = false;
            }
            else if (_isGround)
            {
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        //キャラの左右移動(壁ジャンプの時は左右移動しない)
        //ダッシュと通常のスピードを変える
        if (!_isEnemyDush)
        {
            if (Input.GetButton("Fire3"))
            {
                _rb.velocity = new Vector2(_x * _dushSpeed, _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(_x * _defaultSpeed, _rb.velocity.y);
            }
            FlipX(_x);
        }
    }
    //キャラの向きを変換する
    void FlipX(float x)
    {
        //入力している方向にキャラを向かせる
        if (x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    //エネミーが一定距離に入ったら
    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyPosition = collision.transform.position;
            _cursor.transform.position = _enemyPosition;
            _cursor.SetActive(true);
            _isEnemyRock = true;
        }
    }

    //一定距離から離れたら
    private void OnTriggerExit2D(Collider2D collision)
    {
        _cursor.SetActive(false);
        _isEnemyRock = false;
    }
}