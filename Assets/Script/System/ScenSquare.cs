using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenSquare : MonoBehaviour
{
    public void ScenChange(string changeScene)
    {
        SceneManager.LoadScene(changeScene);
    }
}
