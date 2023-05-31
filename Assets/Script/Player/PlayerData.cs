using UnityEngine;
using UniRx;
using Unity.VisualScripting;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("�v���C���[��HP")] int _hp;
    [SerializeField, Tooltip("�v���C���[�̍U����")] int _power;
    [SerializeField, Tooltip("�v���C���[�̃X�s�[�h")] int _speed;
    [SerializeField, Tooltip("�v���C���[�̃W�����v��")] float _jumpPower;
    [SerializeField, Tooltip("�v���C���[�̓ːi��")] float _enemyDushPower;
    [SerializeField, Tooltip("�v���C���[�̕ǃW�����v")] float _wallJumpPower;

    string _sceneName;

    public string NextScene { get => _sceneName; set => _sceneName = value; }
    public int MaxHp { get => _hp;}
    public int MaxPower { get => _power; set => _power = value; }
    public int MaxSpeed { get => _speed; set => _speed = value; }
    public float JumpPower { get => _jumpPower; }
    public float EnemyDushPower { get => _enemyDushPower; set => _enemyDushPower = value; }
    public float WallJumpPower { get => _wallJumpPower; }

}