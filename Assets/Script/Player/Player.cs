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
    [SerializeField] GameObject _attackEffect;
    Rigidbody2D _rb;
    Animator _anim;
    Vector3 _enemyPosition;
    ParticleSystem _particle;
    PlayerState _state = PlayerState.Ground;
    int _power;
    int _moveSpeed;
    float  _dushAttackSpeed;
    int _hp;
    int _jumpCount;
    float _x;
    float _jumpPower;
    float _wallJumpPower;
    bool _isJump;

    public int HP { get => _hp; set => _hp = value; }
    public int Power { get => _power; set => _power = value; }
    public int Speed { get => _moveSpeed; set => _moveSpeed = value; }
    public float DushAttack { get => _dushAttackSpeed; set => _dushAttackSpeed = value; }
    public PlayerState State { get => _state; set => _state = value; }

    private void Awake()
    {
        PlayerData maxStatus = Resources.Load<PlayerData>("PlayerData");
        _hp = maxStatus.MaxHp;
        _jumpPower = maxStatus.JumpPower;
        _wallJumpPower = maxStatus.WallJumpPower;
    }
    void Start()
    {
        _particle = GetComponent<ParticleSystem>();
        _rb = GetComponent<Rigidbody2D>();
        _anim = GetComponent<Animator>();
    }
    private void FixedUpdate()
    {
        //キャラの左右移動(壁ジャンプの時は左右移動しない)
        if (Input.GetButton("Horizontal") && _state != PlayerState.DushAttack)
        {
            //AudioManager.Instance.PlaySE(AudioManager.SeSoundData.SE.FootSteps);
            Debug.Log("動いてる");
            //ダッシュと通常のスピードを変える
            if (Input.GetButton("Fire3"))
            {
                _rb.velocity = new Vector2(_x * (_moveSpeed + 5), _rb.velocity.y);
            }
            else
            {
                _rb.velocity = new Vector2(_x * _moveSpeed, _rb.velocity.y);
            }
            FlipX(_x);
            if(_isJump)
            {
                _state = PlayerState.WallJump;
                _jumpCount++;
                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
                _isJump = false;
            }
        }
    }
    void Update()
    {
        _power = GameManager.Instance.Power.Value;
        _moveSpeed =  GameManager.Instance.Speed.Value;
        _dushAttackSpeed = GameManager.Instance.DushAttack.Value;
        Debug.Log($"Player  Power = {_power}, Speed = {_moveSpeed}, DushAttack{_dushAttackSpeed}");
        if(_state == PlayerState.WallJump)
        {
            _rb.mass = 2.0f;
        }
        else
        {
            _rb.mass = 1.5f;
        }
        if (_state == PlayerState.Ground)
        {
            _anim.SetFloat("Speed", _rb.velocity.magnitude);
        }
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
            GetComponent<CapsuleCollider2D>().isTrigger = false;
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
            GetComponent<CapsuleCollider2D>().isTrigger = false;
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
            _state = PlayerState.Jump;
            _isJump = true;
        }
        if(_state == PlayerState.EnemyRock && Input.GetButtonDown("Fire2"))
        {
            _state = PlayerState.DushAttack;
            _particle.Play();
            Vector3 dir = (_enemyPosition - transform.position).normalized;
            _rb.AddForce(dir * _dushAttackSpeed, ForceMode2D.Impulse);
            GetComponent<CapsuleCollider2D>().isTrigger = true;
        }
        if (Input.GetButtonDown("Fire1"))
        {
            AudioManager.Instance.PlaySE(AudioManager.SeSoundData.SE.Fire);
            int random = Random.Range(0, 121);
            _anim.Play("attack");
            Instantiate(_attackEffect, _attackArea.transform.position, Quaternion.Euler(new Vector3(0, 0, random * 30)));
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
            GameManager.Instance.gameState = GameManager.GameState.GameOver;
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