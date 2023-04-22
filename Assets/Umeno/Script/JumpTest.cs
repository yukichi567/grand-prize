using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Rigidbody2D))]
public class JumpTest : InstanceSystem<PlayerController>
{
    [Header("�v���C���[�̓����Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�ʏ펞�̃X�s�[�h")] int _defaultSpeed;
    [SerializeField, Tooltip("�_�b�V�����̃X�s�[�h")] int _dushSpeed;
    [SerializeField, Tooltip("�W�����v�̃p���[")] float _jumpPower;
    [Header("�ݒu�����Ray�Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�ݒu�����Ray�̒���")] float _groundRayRange;
    [SerializeField, Tooltip("Ground�̃��C���[")] LayerMask _groundLayer;
    [Header("�G�l�~�[�Ɋւ��鐔�l")]
    [SerializeField, Tooltip("���b�N�I���̃J�[�\��")] GameObject _cursor;
    [Header("�v���C���[�A�^�b�N�Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�A�^�b�N�p�̃I�u�W�F�N�g")] GameObject _attackObject;
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
        //�ڒn����
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        if (hitGround)
        {
            //���I�u�W�F�N�g�ɂ���Judge�֐���bool��ύX����(�ڒn����)
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
        //�L�����̍��E�ړ�(�ǃW�����v�̎��͍��E�ړ����Ȃ�)
        //�_�b�V���ƒʏ�̃X�s�[�h��ς���
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
    //�L�����̌�����ϊ�����
    void FlipX(float x)
    {
        //���͂��Ă�������ɃL��������������
        if (x > 0)
        {
            transform.localScale = new Vector2(Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
        else if (x < 0)
        {
            transform.localScale = new Vector2(-1 * Mathf.Abs(transform.localScale.x), transform.localScale.y);
        }
    }

    //�G�l�~�[����苗���ɓ�������
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

    //��苗�����痣�ꂽ��
    private void OnTriggerExit2D(Collider2D collision)
    {
        _cursor.SetActive(false);
        _isEnemyRock = false;
    }
}