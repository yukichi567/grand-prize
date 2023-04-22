using UnityEngine;

/// <summary>�v���C���[������������N���X</summary>
public class PlayerGrowBase : MonoBehaviour, IPlayerGrow
{
    //GameManager�Ƃ��Ă���
    [SerializeField, Header("��������鐔�l")] protected int[] _stats = new int[5];
    [SerializeField, Header("�����|�C���g")] protected int[] _cost = new int[5];
    [Tooltip("����������")] protected int _count = 0;

    public void GrowPlayerState()
    {

        //�X�L���|�C���g������Ă��狭��
        if (_count < _stats.Length)
        {
            Grow();
            //�X�L���|�C���g���炷
            Debug.Log($"�X�L���|�C���g��{_cost[_count]}����܂����B");
            _count++;
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
