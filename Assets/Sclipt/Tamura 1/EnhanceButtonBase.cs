using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

/// <summary>強化ボタンの基底クラス</summary>
public class EnhanceButtonBase : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler, IPointerDownHandler, IPointerUpHandler
{
    private Image _image = default;
    //GameManagerとってくる
    [SerializeField, Header("強化される数値")] protected int[] _stats = new int[5];
    [SerializeField, Header("消費するポイント")] private int[] _cost = new int[5];
    protected int _count = 0;

    private void Start()
    {
        _image = GetComponent<Image>();
    }

    /// <summary>強化するメソッドを実行する</summary>
    public void OnPointerClick(PointerEventData eventData)
    {

        //スキルポイントが足りてたら強化
        if(_count < _stats.Length)
        {
            Enhance();
            //スキルポイント減らす
            Debug.Log($"スキルポイントを{_cost[_count]}消費しました。");
            _count++;
        }
        else
        {
            Debug.Log("これ以上強化できません。");
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
