using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>強化ボタンにつけるスクリプト</summary>
public class GrowButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _image = default;
    /// <summary>クリックされたときに呼ばれる関数</summary>
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
            Debug.LogError("OnClickedが登録されていません。");
        }
        
    }

    protected virtual void Enhance() { }

    /// <summary>カーソルが乗ったら色を変える</summary>
    public void OnPointerEnter(PointerEventData eventData)
    {
        _image.color = new Color(0.7f, 0.7f, 0.7f, 1);
    }

    /// <summary>カーソルが離れたら色を戻す</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = Color.white;
    }

    /// <summary>クリックされたらたら色を変える</summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = Color.gray;
    }

    /// <summary>クリック後離したら色を戻す</summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = Color.white;
    }

}
