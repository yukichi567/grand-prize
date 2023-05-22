using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.IO;

public class ButtonSystem : MonoBehaviour
{
    [SerializeField] GameObject _main;
    [SerializeField] GameObject _stageSelect;
    [SerializeField] GameObject _help;

    public void SceneLoad(string name)
    {
        SceneManager.LoadScene(name);
    }

    public void StageSelectButton()
    {
        _main.SetActive(false);
        _stageSelect.SetActive(true);
    }

    public void HelpButton()
    {
        _help.SetActive(true);
        _main.SetActive(false);
    }
}