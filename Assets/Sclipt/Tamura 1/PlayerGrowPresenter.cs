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
                if(count < _powerGrow.Costs.Length) //�܂������ł���Ƃ�
                {
                    _powerText.SetGrowInfText(_powerGrow.Stats[count], _powerGrow.Stats[count + 1]);
                    _powerText.SetCostInfText(_powerGrow.Costs[count]);
                }
                else
                {
                    _powerText.SetCompleteText(_powerGrow.Stats[count]);
                }
                
            });

        _speedGrow.Count
            .Subscribe(count =>
            {

                //�e�L�X�g��ύX����
                if (count < _speedGrow.Costs.Length) //�܂������ł���Ƃ�
                {
                    _speedText.SetGrowInfText(_speedGrow.Stats[count], _speedGrow.Stats[count + 1]);
                    _speedText.SetCostInfText(_speedGrow.Costs[count]);
                }
                else
                {
                    _speedText.SetCompleteText(_speedGrow.Stats[count]);
                }

            });

        _chargeSpeedGrow.Count
            .Subscribe(count =>
            {

                //�e�L�X�g��ύX����
                if (count < _chargeSpeedGrow.Costs.Length) //�܂������ł���Ƃ�
                {
                    _chargeSpeedText.SetGrowInfText(_chargeSpeedGrow.Stats[count], _chargeSpeedGrow.Stats[count + 1]);
                    _chargeSpeedText.SetCostInfText(_chargeSpeedGrow.Costs[count]);
                }
                else
                {
                    _chargeSpeedText.SetCompleteText(_chargeSpeedGrow.Stats[count]);
                }

            });

    }

}
