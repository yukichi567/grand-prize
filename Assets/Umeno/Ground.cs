using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class Ground : MonoBehaviour, IJudeSystem
{
    public void GroundJudge()
    {
        PlayerController.Instance.IsGround = true;
    }
}