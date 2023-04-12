using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _limitTime;
    float _nowTime;
    [SerializeField]float _moveSpeed;
    [SerializeField]bool _arrived;
    [SerializeField] float _stopDistance;
    Vector2 _playerPosTmp;
    Vector2 _bosspos;
    float _distance;
    Vector3 _velocity = Vector3.zero; //jump�ړ��̑��x������
    [SerializeField] bool _jumpBool = false;
    // Start is called before the first frame update
    void Start()
    {
        _rb = GetComponent<Rigidbody2D>();
        _playerPosTmp = GameObject.Find("Player").transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        BossQuaternion();
        _nowTime += Time.deltaTime;
        if(_nowTime > _limitTime)
        {
            int numRandam = Random.Range(0,3);
            if(numRandam == 0)
            {
                
            }
            else if(numRandam == 1)
            {
                
            }
            else
            {

            }
        }
        //Attack();
        Attack1();
    }
    void BossQuaternion()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 bossPos = this.transform.position;
        if(playerPos.x < bossPos.x) //Boss�̌����ύX
        {
            this.transform.rotation = new Quaternion(0f, 0f, 0f, 0f);
        }
        else
        {
            this.transform.rotation = new Quaternion(0f, 180f, 0f, 0f);
        }
    }
    void Attack()
    {
        if(_arrived)
        {
           _bosspos  = this.transform.position;
            Vector2 playerPos = GameObject.Find("Player").transform.position;
            _playerPosTmp = playerPos;
            _arrived = false;
            Debug.Log("�Ă΂ꂽ");
        }
        Vector2 dir = (_playerPosTmp - _bosspos).normalized;
        _rb.velocity = dir * _moveSpeed;
        _distance = Vector2.Distance(this.transform.position,GameObject.Find("Player").transform.position);
        Debug.Log(_distance);
        if (_distance < _stopDistance)
        {
            _rb.velocity = Vector2.zero;
            Debug.Log("�U���\��");
        }
    }
    void Attack1()
    {
        if (_arrived)
        {
            _bosspos = this.transform.position;
            Vector2 playerPos = GameObject.Find("Player").transform.position;
            _playerPosTmp = playerPos;
            _arrived = false;
            Debug.Log("�Ă΂ꂽ");
        }
        if(_jumpBool == false)
        {
            this.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_playerPosTmp.x / 2,
            _playerPosTmp.y + 10, 0f), ref _velocity, 0.5f);
            StartCoroutine("JumpAttack");
        }
    }
    void Attack2()
    {

    }
    IEnumerator JumpAttack()
    {
        Debug.Log("�Ă΂�܂�����R���[�`��");
        yield return new WaitForSeconds(2.6f);
        _jumpBool = true;
    }
}
