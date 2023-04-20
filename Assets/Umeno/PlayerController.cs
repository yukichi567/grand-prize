using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PlayerController : InstanceSystem<PlayerController>
{
    [Header("�v���C���[�̓����Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�ʏ펞�̃X�s�[�h")] int _defaultSpeed;
    [SerializeField, Tooltip("�_�b�V�����̃X�s�[�h")] int _dushSpeed;
    [SerializeField, Tooltip("�W�����v�̃p���[")] float _jumpPower;
    [Header("�ǂɂ�����������Ray�Ɋւ��鐔�l")]
    [SerializeField] float _wallRayRange;
    [SerializeField, Tooltip("Ground�̃��C���[")] LayerMask _wallLayer;
    [Header("�ݒu�����Ray�Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�ݒu�����Ray�̒���")] float _groundRayRange;
    [SerializeField, Tooltip("Ground�̃��C���[")] LayerMask _groundLayer;
    [Header("�v���C���[�A�^�b�N�Ɋւ��鐔�l")]
    [SerializeField, Tooltip("�A�^�b�N�p�̃I�u�W�F�N�g")] GameObject _attackObject;
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
        //�ڒn����
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        if (hitGround)
        {
            //���I�u�W�F�N�g�ɂ���Judge�֐���bool��ύX����(�ڒn����)
            _isGround = true;
            _isWallJump = false;
        }
        else
        {
            _isGround = false;
        }

        //�ǂ̔���
        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
        //�E�̕ǂɓ���������
        if (hitWallRight)
        {
            _isGround = false;
            //�ǂɓ���������ԂŃW�����v�����獶��ɔ��
            if (Input.GetButtonDown("Jump"))
            {
                //_rb.velocity = Vector2.zero;
                 _isWallJump = true;
                _rb.velocity = new Vector2(-1, 1).normalized * _jumpPower;
                FlipX(hitWallRight.normal.x);
            }
        }
        //���̕ǂɓ���������
        if (hitWallLeft)
        {
            _isGround = false;
            //�ǂɓ���������ԂŃW�����v������E��ɔ��
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
        //�L�����̍��E�ړ�(�ǃW�����v�̎��͍��E�ړ����Ȃ�)
        if (!_isWallJump)
        {
            //�_�b�V���ƒʏ�̃X�s�[�h��ς���
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