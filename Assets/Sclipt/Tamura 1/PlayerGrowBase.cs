using UnityEngine;
using UniRx;

///<summary>�v���C���[������������N���X</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManager�Ƃ��Ă���
    [SerializeField, Header("��������鐔�l(0�Ԗڂ͏����l)")] protected float[] _stats = new float[6];
    [SerializeField, Header("�����|�C���g")] protected int[] _costs = new int[5];
    [Tooltip("����������")] protected ReactiveProperty<int> _count = new ReactiveProperty<int>(0);
    public IReadOnlyReactiveProperty<int> Count => _count;

    public void GrowPlayerState()
    {

        //�X�L���|�C���g������Ă��狭��
        if (_count.Value < _costs.Length)
        {
            //�X�L���|�C���g���炷
            Debug.Log($"�X�L���|�C���g��{_costs[_count.Value]}����܂����B");
            _count.Value++;
            Grow();
        }
        else
        {
            Debug.Log("����ȏ㋭���ł��܂���B");
        }

    }

    protected virtual void Grow()
    {
        Debug.LogError("�p����Ń��\�b�h���`���Ă��������B");
    }

    /// <summary>���̃X�e�[�^�X�̒l��Ԃ����\�b�h</summary>
    /// <returns></returns>
    public float GetNowState()
    {
        return _stats[_count.Value];
    }

    /// <summary>���̋����̒l��Ԃ����\�b�h</summary>
    /// <returns></returns>
    public float GetNextState()
    {
        return _stats[_count.Value + 1];
    }

    /// <summary>�����ɂ�����R�X�g��Ԃ����\�b�h</summary>
    /// <returns></returns>
    public int GetCost()
    {
        return _costs[_count.Value];
    }

    /// <summary>�ő�܂ł̋�������񐔂�Ԃ����\�b�h</summary>
    /// <returns></returns>
    public int GetMaxGrowCount()
    {
        return _costs.Length;
    }

}
