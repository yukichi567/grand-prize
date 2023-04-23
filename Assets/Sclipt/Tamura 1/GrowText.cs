using UnityEngine;
using UnityEngine.UI;

public class GrowText : MonoBehaviour
{
    [SerializeField] private Text _growInf = default;
    [SerializeField] private Text _costInf = default;

    /// <summary>���̃X�e�[�^�X�ƁA������̃X�e�[�^�X���e�L�X�g�ɕ\������</summary>
    /// <param name="nowState"></param>
    /// <param name="nextState"></param>
    public void SetGrowInfText(int nowState, int nextState)
    {
        _growInf.text = $"{nowState} �� {nextState}";
    }

    /// <summary>�����Ɏg���R�X�g���e�L�X�g�ɕ\������</summary>
    /// <param name="cost"></param>
    public void SetCostInfText(int cost)
    {
        _costInf.text = $"�K�v�|�C���g:{cost}";
    }

    /// <summary>�ő�܂ŋ��������Ƃ��̃e�L�X�g��\������</summary>
    /// <param name="state"></param>
    public void SetCompleteText(int state)
    {
        _growInf.text = $"{state}";
        _costInf.text = $"�ő�܂ŋ����ς�";
    }
}
