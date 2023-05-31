using UnityEngine;
using UniRx;
using Unity.VisualScripting;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("プレイヤーのHP")] int _hp;
    [SerializeField, Tooltip("プレイヤーの攻撃力")] int _power;
    [SerializeField, Tooltip("プレイヤーのスピード")] int _speed;
    [SerializeField, Tooltip("プレイヤーのジャンプ力")] float _jumpPower;
    [SerializeField, Tooltip("プレイヤーの突進力")] float _enemyDushPower;
    [SerializeField, Tooltip("プレイヤーの壁ジャンプ")] float _wallJumpPower;

    string _sceneName;

    public string NextScene { get => _sceneName; set => _sceneName = value; }
    public int MaxHp { get => _hp;}
    public int MaxPower { get => _power; set => _power = value; }
    public int MaxSpeed { get => _speed; set => _speed = value; }
    public float JumpPower { get => _jumpPower; }
    public float EnemyDushPower { get => _enemyDushPower; set => _enemyDushPower = value; }
    public float WallJumpPower { get => _wallJumpPower; }

}