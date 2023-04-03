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
    Rigidbody2D _rb;
    float _x;
    bool _isGround;
    bool _isWallJump;

    public bool IsGround { get => _isGround; set => _isGround = value; }
    public bool IsWallJump { get => _isWallJump; set => _isWallJump = value; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        //設置判定
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        if (hitGround)
        {
            hitGround.collider.gameObject.GetComponent<IJudeSystem>().GroundJudge();
        }
        else
        {
            _isGround = false;
        }

        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
        if (hitWallRight)
        {
            _isGround = false;
            if (Input.GetButtonDown("Jump"))
            {
                hitWallRight.collider.gameObject.GetComponent<IJudeSystem>().GroundJudge();
                Debug.Log("HitRight");
                _rb.velocity = new Vector2(-1, 1).normalized * _jumpPower;
                FlipX(hitWallRight.normal.x);
            }
        }
        if(hitWallLeft)
        {
            _isGround = false;
            if (Input.GetButtonDown("Jump"))
            {
                hitWallLeft.collider.gameObject.GetComponent<IJudeSystem>().GroundJudge();
                _rb.velocity = new Vector2(1, 1).normalized * _jumpPower;
                FlipX(hitWallLeft.normal.x);
            }
        }
        Debug.Log($"Ground{_isGround}:Wall{_isWallJump}");
        if(Input.GetButtonDown("Jump"))
        {
            if (_isGround)
            {
                _isGround = false;
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }
        }
    }

    private void FixedUpdate()
    {
        //キャラの左右移動()
        if (!_isWallJump)
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
    //キャラの動き
    void FlipX(float x)
    {
        //入力している方向にキャラを向かせる
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