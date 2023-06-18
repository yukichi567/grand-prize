using UnityEngine;
using UnityEngine.UI;

public class GrowText : MonoBehaviour
{
    [SerializeField] private Text _growInf = default;
    [SerializeField] private Text _growInf2 = default;
    [SerializeField] private Text _costInf = default;

    /// <summary>今のステータスと、強化後のステータスをテキストに表示する</summary>
    /// <param name="nowState"></param>
    /// <param name="nextState"></param>
    public void SetGrowInfText(float nowState, float nextState)
    {
        _growInf.text = $"{nowState}";
        _growInf2.text = $"{nextState}";
    }

    /// <summary>強化に使うコストをテキストに表示する</summary>
    /// <param name="cost"></param>
    public void SetCostInfText(int cost)
    {
        _costInf.text = $"UsePoint:{cost}";
    }

    /// <summary>最大まで強化したときのテキストを表示する</summary>
    /// <param name="state"></param>
    public void SetCompleteText(float state)
    {
        _growInf.text = $"{state}";
        _costInf.text = $"MaxStrengthen";
    }
}
