using System;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>強化ボタンにつけるスクリプト</summary>
public class GrowButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _image = default;
    private Color _defaltColer = default;
    /// <summary>クリックされたときに呼ばれる関数</summary>
    public Action OnClicked;

    private void Start()
    {
        _image = GetComponent<Image>();
        _defaltColer = _image.color;
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
        _image.color = _defaltColer - new Color(0.3f, 0.3f, 0.3f, 0);
    }

    /// <summary>カーソルが離れたら色を戻す</summary>
    public void OnPointerExit(PointerEventData eventData)
    {
        _image.color = _defaltColer;
    }

    /// <summary>クリックされたらたら色を変える</summary>
    public void OnPointerDown(PointerEventData eventData)
    {
        _image.color = _defaltColer - new Color(0.5f, 0.5f, 0.5f, 0);
    }

    /// <summary>クリック後離したら色を戻す</summary>
    public void OnPointerUp(PointerEventData eventData)
    {
        _image.color = _defaltColer;
    }

}
