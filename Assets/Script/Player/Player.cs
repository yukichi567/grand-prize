using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;
using UniRx;

[RequireComponent(typeof(Rigidbody2D))]
public class Player : InstanceSystem<Player>
{
    [Header("設置判定のRay")]
    [SerializeField] LayerMask _groundLayer;
    [SerializeField] float _groundRayRange;

    [Header("壁のRay")]
    [SerializeField] LayerMask _wallLayer;
    [SerializeField] float _wallRayRange;

    [Header("攻撃に関するオブジェクト")]
    [SerializeField] GameObject _attackArea;
    [SerializeField] GameObject _cursor;
    Rigidbody2D _rb;
    Animator _anim;
    Vector3 _enemyPosition;
    ParticleSystem _particle;
    PlayerState _state = PlayerState.Ground;
    ReactiveProperty<int> _power;
    ReactiveProperty<int> _moveSpeed;
    ReactiveProperty<float> _dushAttackSpeed;
    int _hp;
    int _jumpCount;
    float _x;
    float _jumpPower;
    float _wallJumpPower;

    public int HP { get => _hp; set => _hp = value; }
    public ReactiveProperty<int> Power { get => _power; }
    public PlayerState State { get => _state; set => _state = value; }

    private void Awake()
    {
        PlayerData maxStatus = Resources.Load<PlayerData>("PlayerData");
        _hp = maxStatus.MaxHp;
        _power = maxStatus.MaxPower;
        _moveSpeed = maxStatus.MaxSpeed;
        _jumpPower = maxStatus.JumpPower;
        _dushAttackSpeed = maxStatus.EnemyDushPower;
        _wallJumpPower = maxStatus.WallJumpPower;
    }
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
        this.ObserveEveryValueChanged(x => x._power.Value).Subscribe(newValue => _power.Value = newValue);
        this.ObserveEveryValueChanged(x => x._moveSpeed.Value).Subscribe(newValue => _moveSpeed.Value = newValue);
        this.ObserveEveryValueChanged(x => x._dushAttackSpeed.Value).Subscribe(newValue => _dushAttackSpeed.Value = newValue);
    }
    private void FixedUpdate()
    {
        //キャラの左右移動(壁ジャンプの時は左右移動しない)
        if (Input.GetButton("Horizontal") && _state != PlayerState.DushAttack)
        {
            Debug.Log("動いてる");
            //ダッシュと通常のスピードを変える
            if (Input.GetButton("Fire3"))
            {
                _rb.velocity = new Vector2(_x * (_moveSpeed.Value + 5), _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(_x * _moveSpeed.Value, _rb.velocity.y);
            }
            FlipX(_x);
        }
    }
    void Update()
    {
        _anim.SetFloat("Speed", _rb.velocity.magnitude);
        _anim.SetBool("IsJump", _state == PlayerState.Jump);
        _anim.SetBool("IsWallJump", _state == PlayerState.WallJump);
        _x = Input.GetAxisRaw("Horizontal");
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        if (hitGround)
        {
            _state = PlayerState.Ground;
            GetComponent<CapsuleCollider2D>().isTrigger = false;
            _jumpCount = 0;
        }
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
        //右の壁に当たったら
        if (hitWallRight)
        {
            Debug.Log(_state);
            //壁に当たった状態でジャンプしたら左上に飛ぶ
            if (Input.GetButtonDown("Jump"))
            {
                _state = PlayerState.WallJump;
                //_particle.Play();
                _rb.velocity = new Vector2(-1, 1).normalized * _wallJumpPower;
                FlipX(hitWallRight.normal.x);
            }
        }
        //左の壁に当たったら
        if (hitWallLeft)
        {
            Debug.Log(_state);
            //壁に当たった状態でジャンプしたら右上に飛ぶ
            if (Input.GetButtonDown("Jump"))
            {
                _state = PlayerState.WallJump;
                //_particle.Play();
                _rb.velocity = new Vector2(1, 1).normalized * _wallJumpPower;
                FlipX(hitWallLeft.normal.x);
            }
        }
        if((_jumpCount < 1 || _state == PlayerState.Ground) && Input.GetButtonDown("Jump"))
        {
            _jumpCount++;
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
            _state = PlayerState.Jump;
        }
        if(_state == PlayerState.EnemyRock && Input.GetButtonDown("Fire2"))
        {
            _state = PlayerState.DushAttack;
            _particle.Play();
            Vector3 dir = (_enemyPosition - transform.position).normalized;
            _rb.AddForce(dir * _dushAttackSpeed.Value, ForceMode2D.Impulse);
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            _anim.Play("attack");
        }
    }
    private void OnTriggerStay2D(Collider2D collision)
    {
        //敵が一定距離にいる場合敵の位置を保存する
        if (collision.CompareTag("Enemy"))
        {
            _enemyPosition = collision.transform.position;
            _cursor.transform.position = _enemyPosition;
            _cursor.SetActive(true);
            _state = PlayerState.EnemyRock;
        }
    }

    //コライダーから離れた時カーソルを非表示にする
    private void OnTriggerExit2D(Collider2D collision)
    {
        _cursor.SetActive(false);
        //_state = PlayerState.EnemyRock;
    }
    public void Damage(int damage)
    {
        _hp -= damage;
        if (_hp < 0)
        {
            Destroy(this.gameObject);
        }
    }
    public void AttackStart()
    {
        _attackArea.SetActive(true);
    }
    public void AttackEnd()
    {
        _attackArea.SetActive(false);
    }
    public void FlipX(float x)
    {
        if (x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    public enum PlayerState
    {
        Ground,
        Jump,
        WallJump,
        EnemyRock,
        DushAttack,
    }
}