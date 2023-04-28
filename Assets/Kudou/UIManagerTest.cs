using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManagerTest : MonoBehaviour
{
    [SerializeField, Header("����")] Text _timerText;
    [SerializeField, Header("�n�C�X�R�A")] Text _lowTimerText;
    [SerializeField, Header("�|�C���gText")] Text _pointText;
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
        _pointText.text = $"{gameManager.Point}";
    }
}
