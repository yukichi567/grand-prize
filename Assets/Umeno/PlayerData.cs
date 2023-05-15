using UnityEngine;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("プレイヤーのHP")]int _hp;
    [SerializeField, Tooltip("プレイヤーの攻撃力")] int _power;
    [SerializeField, Tooltip("プレイヤーのスピード")] int _speed;
    [SerializeField, Tooltip("プレイヤーのスピード")] float _jumpPower;
    [SerializeField, Tooltip("プレイヤーのスピード")] float _enemyDushPower;
    [SerializeField, Tooltip("プレイヤーのスピード")] float _wallJumpPower;

    public int MaxHp { get => _hp; set => _hp = value; }
    public int MaxPower { get => _power; set => _power = value; }
    public int MaxSpeed { get => _speed; set => _speed = value; }
    public float JumpPower { get => _jumpPower; }
    public float EnemyDushPower { get => _enemyDushPower; }
    public float WallJumpPower { get => _wallJumpPower; }
}