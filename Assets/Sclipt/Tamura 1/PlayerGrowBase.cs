using UnityEngine;
using UniRx;

///<summary>プレイヤーを強化する基底クラス</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManagerとってくる
    [SerializeField, Header("強化される数値(0番目は初期値)")] protected int[] _stats = new int[6];
    /// <summary>強化される数値(0番目は初期値)</summary>
    public int[] Stats => _stats;
    [SerializeField, Header("消費するポイント")] protected int[] _costs = new int[5];
    /// <summary>消費するポイント</summary>
    public int[] Costs => _costs;
    [Tooltip("強化した回数")] protected ReactiveProperty<int> _count = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Count => _count;

    public void GrowPlayerState()
    {

        //スキルポイントが足りてたら強化
        if (_count.Value < _costs.Length)
        {
            //スキルポイント減らす
            Debug.Log($"スキルポイントを{_costs[_count.Value]}消費しました。");
            _count.Value++;
            Grow();
        }
        else
        {
            Debug.Log("これ以上強化できません。");
        }

    }

    protected virtual void Grow()
    {
        Debug.LogError("継承先でメソッドを定義してください。");
    }

}
