using UnityEngine;

/// <summary>�v���C���[�̓ːi���x����������</summary>
public class PlayerChargeSpeedGrow : PlayerGrowBase
{
    private static float _chargeSpeed = 0;
    public static float ChargeSpeed => _chargeSpeed;

    protected override void Grow()
    {
        _chargeSpeed = _stats[_count];
        Debug.Log($"�ːi�X�s�[�h��{_chargeSpeed}�ɂȂ�܂����B");
    }

}
