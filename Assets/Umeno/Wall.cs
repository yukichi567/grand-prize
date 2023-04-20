using UnityEngine;

public class Wall : MonoBehaviour, IJudge
{
    public void FalseAction()
    {
        PlayerController.Instance.IsWallJump = false;
    }

    public void TrueAction()
    {
        PlayerController.Instance.IsWallJump = true;
    }
}