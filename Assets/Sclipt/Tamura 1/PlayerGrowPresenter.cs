using UnityEngine;

/// <summary>プレイヤーを強化する関数を登録</summary>
public class PlayerGrowPresenter : MonoBehaviour
{
    //view
    [SerializeField] private GrowButton _powerGrowButton = default;
    [SerializeField] private GrowButton _speedGrowButton = default;
    [SerializeField] private GrowButton _chargeSpeedGrowButton = default;

    //model
    [SerializeField] private PlayerGrowBase _powerGrow = default;
    [SerializeField] private PlayerGrowBase _speedGrow = default;
    [SerializeField] private PlayerGrowBase _chargeSpeedGrow = default;
    //インターフェイスはSerializeFieldできないらしい。代わり => SerializeReference

    void Start()
    {
        //OnClickedに関数を登録
        _powerGrowButton.OnClicked = _powerGrow.GrowPlayerState;
        _speedGrowButton.OnClicked = _speedGrow.GrowPlayerState;
        _chargeSpeedGrowButton.OnClicked = _chargeSpeedGrow.GrowPlayerState;
    }

}
