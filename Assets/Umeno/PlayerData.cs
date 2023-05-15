using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("�v���C���[��HP")]int _hp;
    [SerializeField, Tooltip("�v���C���[�̍U����")] int _power;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] int _speed;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] float _jumpPower;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] float _enemyDushPower;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] float _wallJumpPower;

    public int MaxHp { get => _hp; set => _hp = value; }
    public int MaxPower { get => _power; set => _power = value; }
    public int MaxSpeed { get => _speed; set => _speed = value; }
    public float JumpPower { get => _jumpPower; }
    public float EnemyDushPower { get => _enemyDushPower; }
    public float WallJumpPower { get => _wallJumpPower; }
}