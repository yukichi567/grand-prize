using UnityEngine;
using System.Collections;

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
    [Header("エネミーに関する数値")]
    [SerializeField, Tooltip("EnemyまでのRayの長さ")] float _enemyRayRange;
    [SerializeField, Tooltip("エネミーのレイヤー")] LayerMask _enemyLayer;
    [SerializeField, Tooltip("ロックオンのカーソル")] GameObject _cursor;
    [Header("プレイヤーアタックに関する数値")]
    [SerializeField, Tooltip("アタック用のオブジェクト")] GameObject _attackObject;
    Rigidbody2D _rb;
    Vector3 _enemyPosition;
    float _x;
    bool _isGround;
    bool _isWallJump;
    bool _isEnemyRock;

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
            hitGround.collider.gameObject.GetComponent<IJude>().GroundJudge();
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
        if (hitWallRight)
        {
            _isGround = false;
            if (Input.GetButtonDown("Jump"))
            {
                hitWallRight.collider.gameObject.GetComponent<IJude>().GroundJudge();
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
                hitWallLeft.collider.gameObject.GetComponent<IJude>().GroundJudge();
                _rb.velocity = new Vector2(1, 1).normalized * _jumpPower;
                FlipX(hitWallLeft.normal.x);
            }
        }
        if (Input.GetButtonDown("Jump"))
        {
            //設置していてエネミーをロックしていなければ
            if (_isGround && !_isEnemyRock)
            {
                _isGround = false;
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            }
            else
            {
                float rad = Mathf.Atan2(_enemyPosition.y - transform.position.y, _enemyPosition.x - transform.position.y);
                //StartCoroutine(TargetRock(rad));
                StartCoroutine(TargetRock(new Vector3(_enemyPosition.x + 1f, _enemyPosition.y, 0)));
            }
        }
        if(Input.GetButtonDown("Fire1"))
        {
            StartCoroutine(Attack());
            Debug.Log("攻撃中");
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
    private void OnTriggerStay2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            _enemyPosition = collision.transform.position;
            _cursor.transform.position = _enemyPosition;
            _cursor.SetActive(true);
            _isEnemyRock = true;
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _cursor.SetActive(false);
        _isEnemyRock = false;
    }

    IEnumerator TargetRock(float rad)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        //float angle = Vector3.Angle(transform.right, _enemyPosition);
        //transform.rotation = Quaternion.Euler(0f, 0f, angle);
        _rb.AddForce(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad))* _jumpPower, ForceMode2D.Impulse);
        yield return new WaitForSeconds(0.2f);
        transform.rotation = Quaternion.Euler(0f, 0f, 0f);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    IEnumerator TargetRock(Vector3 position)
    {
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
        transform.position = position;
        yield return new WaitForSeconds(0.2f);
        this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    }

    IEnumerator Attack()
    {
        _rb.drag = 100;
        _attackObject.SetActive(true);
        yield return new WaitForSeconds(0.1f);
        _attackObject.SetActive(false);
        yield return new WaitForSeconds(0.5f);
        _rb.drag = 0;
    }
}