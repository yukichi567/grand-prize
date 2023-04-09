using UnityEngine;

public class Wall : MonoBehaviour, IJude
{
    public void GroundJudge()
    {
        PlayerController.Instance.IsWallJump = true;
    }
}