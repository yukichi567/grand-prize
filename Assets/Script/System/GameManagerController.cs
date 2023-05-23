using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>現在のゲームステージシーン</summary>
    [SerializeField,Header("現在のゲームステージシーン")]GameManager.StageNumber _stageNumber;
    /// <summary>次遷移するシーンの名前</summary>
    [SerializeField, Header("次遷移するシーンの名前")] string _nextSceneName;
    GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        _gameManager.NextSceneName = _nextSceneName;
        //現在のシーンがゲームシーンだった時
        if(_stageNumber != GameManager.StageNumber.None)
        {
            //状態をゲーム中に変更
            _gameManager.gameState = GameManager.GameState.Game;
            //難易度によってのEnemyHP最大値と攻撃力を調整
            _gameManager.DifficultyAdjustment();
        }
        else
        {
            //状態をタイトル・難易度選択・その他シーン状態に変更
            _gameManager.gameState = GameManager.GameState.TitleOrDifficultySelect;
        }
    }

    
}
