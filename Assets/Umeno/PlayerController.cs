using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

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
    Rigidbody2D _rb;
    float _x;
    float _y;
    bool _isGround;

    public bool IsGround { get => _isGround; set => _isGround = value; }
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();  
    }

    void Update()
    {
        _x = Input.GetAxisRaw("Horizontal");
        //�ݒu����
        //if(Input.GetButtonDown("Jump"))
        //{
        //    RaycastHit2D hitGround = Physics2D.Raycast(transform.position, Vector2.down, _groundRayRange, (int)_groundLayer);
        //    Debug.DrawRay(transform.position, Vector2.down * _groundRayRange, Color.red);
        //    if (hitGround)
        //    {
        //        hitGround.collider.gameObject.GetComponent<IJudeSystem>().GroundJudge();
        //    }
        //}

        RaycastHit2D hitWallRight = Physics2D.Raycast(transform.position, Vector2.right, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.right * _wallRayRange, Color.blue);
        RaycastHit2D hitWallLeft = Physics2D.Raycast(transform.position, Vector2.left, _wallRayRange, _wallLayer);
        Debug.DrawRay(transform.position, Vector2.left * _wallRayRange, Color.blue);
        if (hitWallRight)
        {
            if (Input.GetButtonDown("Jump"))
            {
                Debug.Log("HitRight");
                _rb.velocity = new Vector2(-10f, 2f) * _jumpPower;
                //_rb.AddForce(new Vector2(-10f, 2f) * _jumpPower, ForceMode2D.Impulse);
                FlipX(hitWallRight.normal.x);
            }
        }
        if(hitWallLeft)
        {
            if(Input.GetButtonDown("Jump"))
            {
                Debug.Log("HitLeft");
                _rb.velocity = new Vector2(10f, 2f) * _jumpPower;
                //_rb.AddForce(new Vector2(10f, 2f) * _jumpPower, ForceMode2D.Impulse);
                FlipX(hitWallRight.normal.x);
            }
        }
    }

    private void FixedUpdate()
    {
        //�L�����̍��E�ړ�()
        if (Input.GetButton("Fire3"))
        {
            _rb.velocity = new Vector2(_x * _dushSpeed, _rb.velocity.y);
        }
        else
        {
            _rb.velocity = new Vector2(_x * _defaultSpeed, _rb.velocity.y);
        }
        FlipX(_x);
        //�L�����̃W�����v
        if (_isGround)
        {
            _isGround = false;
            _rb.AddForce(Vector2.up * _jumpPower, ForceMode2D.Impulse);
        }
    }
    //�L�����̓���
    void FlipX(float x)
    {
        //���͂��Ă�������ɃL��������������
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