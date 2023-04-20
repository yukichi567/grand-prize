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
            if (_isGround)
            {
                //�ڒn���Ă��ăG�l�~�[�����b�N���Ă��Ȃ����
                if (_isEnemyRock)
                {
                    //transform.position = _enemyPosition;
                    var dir = (transform.position - _enemyPosition).normalized;
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

    ////Test1�G�Ɍ������ăW�����v
    //void TargetRock(float rad)
    //{
    //    transform.position = new Vector3(Mathf.Cos(rad), Mathf.Sin(rad), 0);
    //    //_rb.AddForce(new Vector2(Mathf.Cos(rad), Mathf.Sin(rad)) * _jumpPower, ForceMode2D.Impulse);
    //}

    ////Test2�G�̑O�ɏu�Ԉړ�
    //IEnumerator TargetRock(Vector3 position)
    //{
    //    this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 0);
    //    transform.position = position;
    //    yield return new WaitForSeconds(0.2f);
    //    this.GetComponent<SpriteRenderer>().color = new Color(255, 255, 255, 255);
    //}
}