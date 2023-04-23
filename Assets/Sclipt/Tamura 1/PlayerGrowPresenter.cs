using UniRx;
using UnityEngine;

/// <summary>プレイヤーを強化する関数を登録</summary>
public class PlayerGrowPresenter : MonoBehaviour
{
    //view
    [SerializeField] private GrowButton _powerGrowButton = default;
    [SerializeField] private GrowButton _speedGrowButton = default;
    [SerializeField] private GrowButton _chargeSpeedGrowButton = default;

    [SerializeField] private GrowText _powerText = default;
    [SerializeField] private GrowText _speedText = default;
    [SerializeField] private GrowText _chargeSpeedText = default;

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

        //countに関数を登録
        _powerGrow.Count
            .Subscribe(count => 
            {

                //テキストを変更する
                if(count < _powerGrow.GetMaxGrowCount()) //まだ強化できるとき
                {
                    _powerText.SetGrowInfText(_powerGrow.GetNowState(), _powerGrow.GetNextState());
                    _powerText.SetCostInfText(_powerGrow.GetCost());
                }
                else
                {
                    _powerText.SetCompleteText(_powerGrow.GetNowState());
                }
                
            });

        _speedGrow.Count
            .Subscribe(count =>
            {

                //テキストを変更する
                if (count < _speedGrow.GetMaxGrowCount()) //まだ強化できるとき
                {
                    _speedText.SetGrowInfText(_speedGrow.GetNowState(), _speedGrow.GetNextState());
                    _speedText.SetCostInfText(_speedGrow.GetCost());
                }
                else
                {
                    _speedText.SetCompleteText(_speedGrow.GetNowState());
                }

            });

        _chargeSpeedGrow.Count
            .Subscribe(count =>
            {

                //テキストを変更する
                if (count < _chargeSpeedGrow.GetMaxGrowCount()) //まだ強化できるとき
                {
                    _chargeSpeedText.SetGrowInfText(_chargeSpeedGrow.GetNowState(), _chargeSpeedGrow.GetNextState());
                    _chargeSpeedText.SetCostInfText(_chargeSpeedGrow.GetCost());
                }
                else
                {
                    _chargeSpeedText.SetCompleteText(_chargeSpeedGrow.GetNowState());
                }

            });

    }

}
