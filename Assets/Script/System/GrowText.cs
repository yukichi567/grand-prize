using UnityEngine;
using UnityEngine.UI;

public class GrowText : MonoBehaviour
{
    [SerializeField] private Text _growInf = default;
    [SerializeField] private Text _growInf2 = default;
    [SerializeField] private Text _costInf = default;

    /// <summary>���̃X�e�[�^�X�ƁA������̃X�e�[�^�X���e�L�X�g�ɕ\������</summary>
    /// <param name="nowState"></param>
    /// <param name="nextState"></param>
    public void SetGrowInfText(float nowState, float nextState)
    {
        _growInf.text = $"{nowState}";
        _growInf2.text = $"{nextState}";
    }

    /// <summary>�����Ɏg���R�X�g���e�L�X�g�ɕ\������</summary>
    /// <param name="cost"></param>
    public void SetCostInfText(int cost)
    {
        _costInf.text = $"UsePoint:{cost}";
    }

    /// <summary>�ő�܂ŋ��������Ƃ��̃e�L�X�g��\������</summary>
    /// <param name="state"></param>
    public void SetCompleteText(float state)
    {
        _growInf.text = $"{state}";
        _costInf.text = $"MaxStrengthen";
    }
}
