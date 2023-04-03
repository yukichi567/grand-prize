using UnityEngine;

public class Wall : MonoBehaviour, IJudeSystem
{
    public void GroundJudge()
    {
        PlayerController.Instance.IsWallJump = true;
    }
}