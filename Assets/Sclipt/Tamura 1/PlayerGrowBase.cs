using UnityEngine;
using UniRx;

///<summary>�v���C���[������������N���X</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManager�Ƃ��Ă���
    [SerializeField, Header("��������鐔�l(0�Ԗڂ͏����l)")] protected int[] _stats = new int[6];
    /// <summary>��������鐔�l(0�Ԗڂ͏����l)</summary>
    public int[] Stats => _stats;
    [SerializeField, Header("�����|�C���g")] protected int[] _costs = new int[5];
    /// <summary>�����|�C���g</summary>
    public int[] Costs => _costs;
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

}
