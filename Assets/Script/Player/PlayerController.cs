﻿//using UnityEngine;
//using System.Collections.Generic;
//using System.Collections;
//using UniRx;
//using System.Security.Cryptography.X509Certificates;
//using UnityEngine.Pool;

//[RequireComponent(typeof(Rigidbody2D))]
//public class PlayerController : InstanceSystem<PlayerController>
//{
//    //ObjectPool<GameObject> pool = new ObjectPool<GameObject>();
//    [Header("設置判定のRayに関する数値")]
//    [SerializeField, Tooltip("設置判定のRayの長さ")] float _groundRayRange;
//    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _groundLayer;
//    [Header("壁にあたった時のRayに関する数値")]
//    [SerializeField, Tooltip("左右のRayの長さ(Wall判定)")] float _wallRayRange;
//    [SerializeField, Tooltip("Groundのレイヤー")] LayerMask _wallLayer;
//    [Header("プレイヤーの動きに関する変数")]
//    [SerializeField, Tooltip("アタック用のオブジェクト")] GameObject _attackObject;
//    [SerializeField, Tooltip("エネミーロックのオブジェクト")] GameObject _cursor;
//    Rigidbody2D _rb;
//    Animator _anim;
//    Vector3 _enemyPosition;
//    GameObject _targetEnemy;
//    ParticleSystem _particle;
//    ReactiveProperty<int> _power;
//    ReactiveProperty<int> _moveSpeed;
//    ReactiveProperty<float> _enemyDushPower;
//    float _x;
//    int _defaultSpeed;
//    int _dushSpeed;
//    int _hp;
//    float _jumpPower;
//    float _wallJumpPower;
//    bool _isGround;
//    bool _isWallJump;
//    bool _isEnemyRock;
//    bool _isEnemyDush;

//    public int HP { get =>  _hp; set => _hp = value;}
//    public ReactiveProperty<int> Power { get => _power; }

//    private void Awake()
//    {
//        PlayerData maxStatus = Resources.Load<PlayerData>("PlayerData");
//        _hp = maxStatus.MaxHp;
//        _power = maxStatus.MaxPower;
//        _moveSpeed = maxStatus.MaxSpeed;
//        _jumpPower = maxStatus.JumpPower;
//        _enemyDushPower = maxStatus.EnemyDushPower;
//        _wallJumpPower = maxStatus.WallJumpPower;
//    }
//    void Start()
//    {
//        _particle = GetComponent<ParticleSystem>();
//        _rb = GetComponent<Rigidbody2D>();
//        _anim = GetComponent<Animator>();
//        _defaultSpeed = _moveSpeed.Value;
//        _dushSpeed = _moveSpeed.Value + 5;
//        this.ObserveEveryValueChanged(x => x._power.Value).Subscribe(newValue => _power.Value = newValue);
//        this.ObserveEveryValueChanged(x => x._moveSpeed.Value).Subscribe(newValue => { _defaultSpeed = newValue; _dushSpeed = newValue + 5; }) ;
//        this.ObserveEveryValueChanged(x => x._enemyDushPower.Value).Subscribe(newValue => _enemyDushPower.Value = newValue);
//    }

//    void Update()
//    {
//        _x = Input.GetAxisRaw("Horizontal");
//        _anim.SetFloat("Speed", _rb.velocity.magnitude);
//        _anim.SetBool("IsJump", _isGround); 
//        _anim.SetBool("IsWallJump", _isWallJump);
//        //接地判定
//        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
//        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
//        if (hitGround)
//        {
//            //床オブジェクトにあるJudge関数でboolを変更する(接地判定)
//            _isGround = true;
//            _isWallJump = false;
//            GetComponent<CapsuleCollider2D>().isTrigger = false;
//        }
//        else
//        {
//            _isGround = false;
//        }

//        if(_isWallJump)
//        {
//            _particle.Play();
//        }
//        //壁の判定
//        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
//        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
//        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
//        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
//        //右の壁に当たったら
//        if (hitWallRight)
//        {
//            _isGround = false;
//            //壁に当たった状態でジャンプしたら左上に飛ぶ
//            if (Input.GetButtonDown("Jump"))
//            {
//                _isWallJump = true;
//                _particle.Play();
//                _rb.velocity = new Vector2(-1, 1).normalized * _wallJumpPower;
//                FlipX(hitWallRight.normal.x);
//            }
//        }
//        //左の壁に当たったら
//        if (hitWallLeft)
//        {
//            _isGround = false;
//            //壁に当たった状態でジャンプしたら右上に飛ぶ
//            if (Input.GetButtonDown("Jump"))
//            {
//                _isWallJump = true;
//                _particle.Play();
//                _rb.velocity = new Vector2(1, 1).normalized * _wallJumpPower;
//                FlipX(hitWallLeft.normal.x);
//            }
//        }
//        if (Input.GetButtonDown("Jump"))
//        {
//            if (_isEnemyRock)
//            {
//                _rb.velocity = Vector2.zero;
//                _isEnemyDush = true;
//                Vector3 dir = (_enemyPosition - transform.position).normalized;
//                //_rb.velocity = dir * _enemyDushPower;
//                _rb.AddForce(dir * _enemyDushPower.Value, ForceMode2D.Impulse);
//                GetComponent<CapsuleCollider2D>().isTrigger = true;
//                StartCoroutine(EnemyDush());
//            }
//            else if (_isGround)
//            {
//                _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
//            }
//        }
//        float enemyDistance = Vector3.Distance(_enemyPosition, transform.position);
//        //if(enemyDistance < 0.5f && _isEnemyRock)
//        //{
//        //    Debug.Log("敵を倒した");
//        //    _targetEnemy.GetComponent<EnemyBase>().Damage(_power.Value * 2);
//        //    _rb.velocity = Vector3.zero;
//        //}
//        if(Input.GetButtonDown("Fire1"))
//        {
//            _anim.Play("attack");
//        }
//    }

//    public void AttackStart()
//    {
//        _attackObject.SetActive(true);
//    }
//    public void AttackEnd()
//    {
//        _attackObject.SetActive(false);
//    }
//    //敵に突進する攻撃　
//    IEnumerator EnemyDush()
//    {
//        yield return new WaitForSeconds(0.5f);
//        _isEnemyDush = false;
//    }

//    private void FixedUpdate()
//    {
//        //キャラの左右移動(壁ジャンプの時は左右移動しない)
//        if (!_isWallJump && !_isEnemyDush)
//        {
//            //ダッシュと通常のスピードを変える
//            if (Input.GetButton("Fire3"))
//            {
//                _rb.velocity = new Vector2(_x * _dushSpeed, _rb.velocity.y);
//            }
//            else
//            {
//                _rb.velocity = new Vector2(_x * _defaultSpeed, _rb.velocity.y);
//            }
//            FlipX(_x);
//        }
//    }
//    //キャラの向きを変換する
//    void FlipX(float x)
//    {
//        //入力している方向にキャラを向かせる
//        if (x > 0)
//        {
//            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
//        }
//        else if (x < 0)
//        {
//            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
//        }
//    }

//    public void Damage(int damage)
//    {
//        _hp -= damage;
//        if(_hp < 0)
//        {
//            Destroy(this.gameObject);
//        }
//    }

//    public enum PlayerState
//    {
//        Jump,
//        WallJump,
//        EnemyRock,
//        DushAttack,
//    }

//    private void OnCollisionEnter2D(Collision2D collision)
//    {
//        if(collision.gameObject.tag == "Enemy")
//        {
//            _targetEnemy.GetComponent<EnemyBase>().Damage(_power.Value * 5);
//            //_rb.velocity = Vector3.zero;
//        }
//    }
//    private void OnTriggerStay2D(Collider2D collision)
//    {
//        //敵が一定距離にいる場合敵の位置を保存する
//        if (collision.CompareTag("Enemy"))
//        {
//            _targetEnemy = collision.gameObject;
//            _enemyPosition = _targetEnemy.transform.position;
//            _cursor.transform.position = _enemyPosition;
//            _cursor.SetActive(true);
//            _isEnemyRock = true;
//        }
//    }

//    //コライダーから離れた時カーソルを非表示にする
//    private void OnTriggerExit2D(Collider2D collision)
//    {
//        _cursor.SetActive(false);
//        _isEnemyRock = false;
//    }
//}