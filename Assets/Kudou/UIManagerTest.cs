using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTest : MonoBehaviour
{
    [SerializeField, Header("時間")] Text _timerText;
    [SerializeField, Header("ハイスコア")] Text _lowTimerText;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        gameManager = GameManager.Instance;
        if(gameManager.LowerTimeLoad() == 0)
        {
            _lowTimerText.text = "";
        }
        else
        {
            _lowTimerText.text = $"{gameManager.LowerTimeLoad()}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timerText.text = $"{(int)gameManager.Timer}";
    }
}
