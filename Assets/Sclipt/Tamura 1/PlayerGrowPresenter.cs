using UnityEngine;

/// <summary>�v���C���[����������֐���o�^</summary>
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
    //�C���^�[�t�F�C�X��SerializeField�ł��Ȃ��炵���B���� => SerializeReference

    void Start()
    {
        //OnClicked�Ɋ֐���o�^
        _powerGrowButton.OnClicked = _powerGrow.GrowPlayerState;
        _speedGrowButton.OnClicked = _speedGrow.GrowPlayerState;
        _chargeSpeedGrowButton.OnClicked = _chargeSpeedGrow.GrowPlayerState;
    }

}
