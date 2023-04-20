using UnityEngine;

public class Ground : MonoBehaviour, IJudge
{
    public void TrueAction()
    {
        PlayerController.Instance.IsGround = true;
    }
    public void FalseAction()
    {
        PlayerController.Instance.IsGround = false;
    }
}