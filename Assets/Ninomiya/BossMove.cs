using System.Collections;
using UnityEngine;

public class BossMove : MonoBehaviour
{
    Rigidbody2D _rb;
    [SerializeField] float _limitTime; //���̍U���܂ł̎���
    float _nowTime;�@//���̍U���܂ł̎���
    [SerializeField] float _moveSpeed; //boss�̈ړ����x
    [SerializeField] bool _arrived; //�U���̍ہA��񂾂�������邽�߂�bool
    [SerializeField] float _stopDistance; //player�ɋ߂Â�����
    Vector2 _playerPosTmp; //player�̈ʒu�擾
    Vector2 _bosspos; //boss�̈ʒu�擾
    float _distance; //boss��player�̋���
    Vector3 _velocity = Vector3.zero; //jump�ړ��̑��x������
    [SerializeField] bool _jumpBool = false;
    [SerializeField] Transform[] _enemyTransform; //�G�����҂���ꏊ
    [SerializeField] GameObject[] _enemys; //�G������

    [SerializeField] bool _attackBool = false;
    int numRandom;
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
        if (_attackBool == false)
        {
            _arrived = true;
            numRandom = UnityEngine.Random.Range(0, 3);
            Debug.Log(numRandom);
            StartCoroutine("AttackTime");
        }
        if (numRandom == 0)
        {
            Attack();
        }
        else if (numRandom == 1)
        {
            Attack1();
        }
        else if (numRandom == 2)
        {
            Attack2();
        }

    }
    void BossQuaternion()
    {
        Vector2 playerPos = GameObject.Find("Player").transform.position;
        Vector2 bossPos = this.transform.position;
        if (playerPos.x < bossPos.x) //Boss�̌����ύX
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
        if (_arrived == true)
        {
            _bosspos = this.transform.position;
            Vector2 playerPos = GameObject.Find("Player").transform.position;
            _playerPosTmp = playerPos;
            _arrived = false;
            Debug.Log("�Ă΂ꂽ");
        }
        Vector2 dir = (_playerPosTmp - _bosspos).normalized;
        _rb.velocity = dir * _moveSpeed;
        _distance = Vector2.Distance(this.transform.position, GameObject.Find("Player").transform.position);
        //Debug.Log(_distance);
        if (_distance < _stopDistance)
        {
            _rb.velocity = Vector2.zero;
            Debug.Log("�U���\��");
        }
    }
    void Attack1()
    {
        if (_arrived == true)
        {
            _bosspos = this.transform.position;
            Vector2 playerPos = GameObject.Find("Player").transform.position;
            _playerPosTmp = playerPos;
            _arrived = false;
            Debug.Log("�Ă΂ꂽ");
        }
        if (_jumpBool == false)
        {
            this.transform.position = Vector3.SmoothDamp(transform.position, new Vector3(_playerPosTmp.x / 2,
            _playerPosTmp.y + 10, 0f), ref _velocity, 0.5f);
            StartCoroutine("JumpAttack");
        }
    }
    void Attack2()
    {
        for (int i = 0; i < _enemyTransform.Length; i++)
        {
            int enemyInstanceCount = UnityEngine.Random.Range(0, _enemys.Length);
            Instantiate(_enemys[enemyInstanceCount], _enemyTransform[enemyInstanceCount]);
        }
    }
    IEnumerator JumpAttack()
    {
        //Debug.Log("�Ă΂�܂�����R���[�`��");
        yield return new WaitForSeconds(1.5f);
        _jumpBool = true;
    }
    IEnumerator AttackTime()
    {
        _attackBool = true;
        yield return new WaitForSeconds(3f);
        Debug.Log("�Ă΂�܂�����R���[�`��");
        _attackBool = false;
    }
}
