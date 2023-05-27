using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenSquare : SingletonMonoBehaviour<ScenSquare>
{
    string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    public void ScenChange(string changeScene)
    {
        if (changeScene != null)
        {
            SceneManager.LoadScene(changeScene);
        }
        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
