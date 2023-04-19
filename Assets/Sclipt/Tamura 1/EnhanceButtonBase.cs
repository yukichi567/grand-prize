using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>�����{�^���̊��N���X</summary>
public class EnhanceButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _image = default;
    //GameManager�Ƃ��Ă���
    [SerializeField, Header("��������鐔�l")] protected int[] _stats = new int[5];
    [SerializeField, Header("�����|�C���g")] private int[] _cost = new int[5];
    protected int _count = 0;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>�������郁�\�b�h�����s����</summary>
    public void OnPointerClick(PointerEventData eventData)
    {

        //�X�L���|�C���g������Ă��狭��
        if(_count < _stats.Length)
        {
            Enhance();
            //�X�L���|�C���g���炷
            Debug.Log($"�X�L���|�C���g��{_cost[_count]}����܂����B");
            _count++;
        }
        else
        {
            Debug.Log("����ȏ㋭���ł��܂���B");
        }
        
    }

    protected virtual void Enhance() { }

    /// <summary>�J�[�\�����������F��ς���</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = new Color(0.7f, 0.7f, 0.7f, 1);
    }

    /// <summary>�J�[�\�������ꂽ��F��߂�</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.white;
    }

    /// <summary>�N���b�N���ꂽ�炽��F��ς���</summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = Color.gray;
    }

    /// <summary>�N���b�N�㗣������F��߂�</summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = Color.white;
    }

}
