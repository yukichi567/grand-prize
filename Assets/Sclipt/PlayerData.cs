using UnityEngine;
using UniRx;
using Unity.VisualScripting;

[CreateAssetMenu(fileName ="PlayerData", menuName = "Player")]
public class PlayerData : ScriptableObject
{
    [SerializeField, Tooltip("プレイヤーのHP")] int _hp;
    [SerializeField, Tooltip("プレイヤーの攻撃力")] ReactiveProperty<int> _power;
    [SerializeField, Tooltip("プレイヤーのスピード")] ReactiveProperty<int> _speed;
    [SerializeField, Tooltip("プレイヤーのジャンプ力")] float _jumpPower;
    [SerializeField, Tooltip("プレイヤーの突進力")] ReactiveProperty<float> _enemyDushPower;
    [SerializeField, Tooltip("プレイヤーの壁ジャンプ")] float _wallJumpPower;

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