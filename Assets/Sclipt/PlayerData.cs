using UnityEngine;
using UniRx;
using Unity.VisualScripting;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("�v���C���[��HP")] int _hp;
    [SerializeField, Tooltip("�v���C���[�̍U����")] ReactiveProperty<int> _power;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] ReactiveProperty<int> _speed;
    [SerializeField, Tooltip("�v���C���[�̃W�����v��")] float _jumpPower;
    [SerializeField, Tooltip("�v���C���[�̓ːi��")] ReactiveProperty<float> _enemyDushPower;
    [SerializeField, Tooltip("�v���C���[�̕ǃW�����v")] float _wallJumpPower;

    public int MaxHp { get => _hp;}
    public ReactiveProperty<int> MaxPower { get => _power; set => _power = value; }
    public ReactiveProperty<int> MaxSpeed { get => _speed; set => _speed = value; }
    public float JumpPower { get => _jumpPower; }
    public ReactiveProperty<float> EnemyDushPower { get => _enemyDushPower; set => _enemyDushPower = value; }
    public float WallJumpPower { get => _wallJumpPower; }

    private void Awake()
    {
        
    }

}