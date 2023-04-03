using UnityEngine;

public class Ground : MonoBehaviour, IJudeSystem
{
    public void GroundJudge()
    {
        PlayerController.Instance.IsGround = true;
        PlayerController.Instance.IsWallJump = false;
    }
}