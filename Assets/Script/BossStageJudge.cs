using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class BossStageJudge : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        if (FindObjectsOfType<BossMove>().Length < 1)
        {
            FindObjectOfType<ScenSquare>().ScenChange("Title");
        }
    }
}