using UnityEngine;

/// <summary>�v���C���[�̓ːi���x����������</summary>
public class PlayerChargeSpeedGrow : PlayerGrowBase
{
    private static float _chargeSpeed = 0;
    public static float ChargeSpeed => _chargeSpeed;
    private static int _chargeGrowCount = 0;

    private void Start()
    {
        _count.Value = _chargeGrowCount;
    }

    protected override void Grow()
    {
        PlayerData data = Resources.Load<PlayerData>("PlayerData");
        data.EnemyDushPower.Value += 10;
        _chargeGrowCount++;
        _chargeSpeed = _stats[_chargeGrowCount];
        Debug.Log($"�ːi�X�s�[�h��{_chargeSpeed}�ɂȂ�܂����B");
    }

}
