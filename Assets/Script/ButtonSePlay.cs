using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonSePlay : MonoBehaviour
{
    public void SePlay()
    {
        AudioManager.Instance.PlaySE(AudioManager.SeSoundData.SE.Button);
    }
}