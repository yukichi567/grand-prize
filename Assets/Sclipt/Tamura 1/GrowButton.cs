using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>�����{�^���ɂ���X�N���v�g</summary>
public class GrowButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _image = default;
    /// <summary>�N���b�N���ꂽ�Ƃ��ɌĂ΂��֐�</summary>
    public Action OnClicked;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    public void OnPointerClick(PointerEventData eventData)
    {

        if(OnClicked != null)
        {
            OnClicked();
        }
        else
        {
            Debug.LogError("OnClicked���o�^����Ă��܂���B");
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
