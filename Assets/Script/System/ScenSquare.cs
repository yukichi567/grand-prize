using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ScenSquare : MonoBehaviour
{
    string _sceneName;
    public string SceneName { get => _sceneName; set => _sceneName = value; }

    private void Awake()
    {
        _sceneName = Resources.Load<PlayerData>("PlayerData").NextScene;
    }
    public void ScenChange(string changeScene)
    {
        Debug.Log("シーン移動");
        if (changeScene != "")
        {
            SceneManager.LoadScene(changeScene);
        }
        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }
}
