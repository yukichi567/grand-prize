using UnityEngine;
using UnityEngine.UI;

public class GrowText : MonoBehaviour
{
    [SerializeField] private Text _growInf = default;
    [SerializeField] private Text _costInf = default;

    /// <summary>今のステータスと、強化後のステータスをテキストに表示する</summary>
    /// <param name="nowState"></param>
    /// <param name="nextState"></param>
    public void SetGrowInfText(int nowState, int nextState)
    {
        _growInf.text = $"{nowState} → {nextState}";
    }

    /// <summary>強化に使うコストをテキストに表示する</summary>
    /// <param name="cost"></param>
    public void SetCostInfText(int cost)
    {
        _costInf.text = $"必要ポイント:{cost}";
    }

    /// <summary>最大まで強化したときのテキストを表示する</summary>
    /// <param name="state"></param>
    public void SetCompleteText(int state)
    {
        _growInf.text = $"{state}";
        _costInf.text = $"最大まで強化済み";
    }
}
