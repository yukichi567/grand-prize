using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : InstanceSystem<PlayerController>
{
    [Header("プレイヤーの動きに関する数値")]
    [SerializeField, Tooltip("通常時のスピード")] int _defaultSpeed;
    [SerializeField, Tooltip("ダッシュ時のスピード")] int _dushSpeed;
    [SerializeField, Tooltip("ジャンプのパワー")] float _jumpPower;
    [Header("壁にあたった時のRayに関する数値")]
    [SerializeField] float _wallRayRange;
    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _wallLayer;
    [Header("設置判定のRayに関する数値")]
    [SerializeField, Tooltip("設置判定のRayの長さ")] float _groundRayRange;
    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _groundLayer;
    [Header("プレイヤーアタックに関する数値")]
    [SerializeField, Tooltip("アタック用のオブジェクト")] GameObject _attackObject;
    [SerializeField] GameObject _cursor;
    Rigidbody2D _rb;
    Vector3 _enemyPosition;
    Animator _anim;
    float _x;
    bool _isGround;
    bool _isWallJump;
    bool _isEnemy;

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
            _isWallJump = false;
        }
        else
        {
            _isGround = false;
        }

        //壁の判定
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
        //右の壁に当たったら
        if (hitWallRight)
        {
            _isGround = false;
            //壁に当たった状態でジャンプしたら左上に飛ぶ
            if (Input.GetButtonDown("Jump"))
            {
                //_rb.velocity = Vector2.zero;
                 _isWallJump = true;
                _rb.velocity = new Vector2(-1, 1).normalized * _jumpPower;
                FlipX(hitWallRight.normal.x);
            }
        }
        //左の壁に当たったら
        if (hitWallLeft)
        {
            _isGround = false;
            //壁に当たった状態でジャンプしたら右上に飛ぶ
            if (Input.GetButtonDown("Jump"))
            {
                //_rb.velocity = Vector2.zero;
                _isWallJump = true;
                _rb.velocity = new Vector2(1, 1).normalized * _jumpPower;
                FlipX(hitWallLeft.normal.x);
            }
            
        }
        if (Input.GetButtonDown("Jump"))
        {
            if (_isGround)
            {
                if (_isEnemy)
                {
                    _rb.velocity = Vector2.zero;
                    var dir = (_enemyPosition - transform.position).normalized;
                    _rb.AddForce(dir * _jumpPower, ForceMode2D.Impulse);
                }
                else
                {
                    _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                }
            }
        }
    }

    private void FixedUpdate()
    {
        //キャラの左右移動(壁ジャンプの時は左右移動しない)
        if (!_isWallJump)
        {
            //ダッシュと通常のスピードを変える
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _isEnemy = true;
            _enemyPosition = collision.gameObject.transform.position;
            _cursor.SetActive(true);
            _cursor.transform.position = _enemyPosition;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _isEnemy = false;
        _cursor.SetActive(false);
    }
}