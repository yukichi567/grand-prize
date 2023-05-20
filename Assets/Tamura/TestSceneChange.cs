using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TestSceneChange : MonoBehaviour
{
    [SerializeField] private int _plusPoint = 100;
    [SerializeField] private SkilPointText _text = default;

    public void TamuraSceneLoad()
    {
        SceneManager.LoadScene("Tamura");
    }

    public void PlusSkilPoint()
    {
        GameManager.Instance.PointPlus(_plusPoint);
        _text.SetSkilPointText(GameManager.Instance.Point);
    }

}
