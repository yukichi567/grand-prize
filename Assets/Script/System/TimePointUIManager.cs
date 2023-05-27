using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimePointUIManager : MonoBehaviour
{
    Text _timerText;
    Text _lowTimerText;
    Text _pointText;
    GameManager gameManager;
    // Start is called before the first frame update
    void Start()
    {
        _timerText = transform.GetChild(0).GetChild(0).GetComponent<Text>();
        _lowTimerText = transform.GetChild(1).GetComponent<Text>();
        _pointText = transform.GetChild(2).GetChild(0).GetComponent<Text>();
        gameManager = GameManager.Instance;
        if(gameManager.LowerTimeLoad() == 0)
        {
            _lowTimerText.text = "";
        }
        else
        {
            _lowTimerText.text = $"{gameManager.LowerTimeLoad().ToString("F2")}";
        }
    }

    // Update is called once per frame
    void Update()
    {
        _timerText.text = $"{gameManager.Timer.ToString("F2")}";
        _pointText.text = $"{gameManager.Point.ToString("d5")}";
    }
}
