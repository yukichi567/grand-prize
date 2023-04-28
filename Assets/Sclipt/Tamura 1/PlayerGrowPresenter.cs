using UniRx;
using UnityEngine;

/// <summary>�v���C���[����������֐���o�^</summary>
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
    //�C���^�[�t�F�C�X��SerializeField�ł��Ȃ��炵���B���� => SerializeReference

    void Start()
    {
        //OnClicked�Ɋ֐���o�^
        _powerGrowButton.OnClicked = _powerGrow.GrowPlayerState;
        _speedGrowButton.OnClicked = _speedGrow.GrowPlayerState;
        _chargeSpeedGrowButton.OnClicked = _chargeSpeedGrow.GrowPlayerState;

        //count�Ɋ֐���o�^
        _powerGrow.Count
            .Subscribe(count => 
            {

                //�e�L�X�g��ύX����
                if(count < _powerGrow.GetMaxGrowCount()) //�܂������ł���Ƃ�
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

                //�e�L�X�g��ύX����
                if (count < _speedGrow.GetMaxGrowCount()) //�܂������ł���Ƃ�
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

                //�e�L�X�g��ύX����
                if (count < _chargeSpeedGrow.GetMaxGrowCount()) //�܂������ł���Ƃ�
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
