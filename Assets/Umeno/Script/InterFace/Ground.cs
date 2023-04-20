using UnityEngine;

public class Ground : MonoBehaviour, IJude
{
    public void GroundJudge()
    {
        PlayerController.Instance.IsGround = true;
        PlayerController.Instance.IsWallJump = false;
    }
}