using UnityEngine;

/// <summary>プレイヤーを強化する基底クラス</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManagerとってくる
    [SerializeField, Header("強化される数値")] protected int[] _stats = new int[5];
    [SerializeField, Header("消費するポイント")] protected int[] _cost = new int[5];
    [Tooltip("強化した回数")] protected int _count = 0;

    public void GrowPlayerState()
    {

        //スキルポイントが足りてたら強化
        if (_count < _stats.Length)
        {
            Grow();
            //スキルポイント減らす
            Debug.Log($"スキルポイントを{_cost[_count]}消費しました。");
            _count++;
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
