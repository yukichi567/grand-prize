using UnityEngine;
using UniRx;

///<summary>プレイヤーを強化する基底クラス</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManagerとってくる
    [SerializeField, Header("強化される数値(0番目は初期値)")] protected float[] _stats = new float[6];
    [SerializeField, Header("消費するポイント")] protected int[] _costs = new int[5];
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

    /// <summary>今のステータスの値を返すメソッド</summary>
    /// <returns></returns>
    public float GetNowState()
    {
        return _stats[_count.Value];
    }

    /// <summary>次の強化の値を返すメソッド</summary>
    /// <returns></returns>
    public float GetNextState()
    {
        return _stats[_count.Value + 1];
    }

    /// <summary>強化にかかるコストを返すメソッド</summary>
    /// <returns></returns>
    public int GetCost()
    {
        return _costs[_count.Value];
    }

    /// <summary>最大までの強化する回数を返すメソッド</summary>
    /// <returns></returns>
    public int GetMaxGrowCount()
    {
        return _costs.Length;
    }

}
