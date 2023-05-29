using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>現在のゲームステージシーン</summary>
    [SerializeField, Header("現在のゲームステージシーン")] GameManager.StageNumber _stageNumber;
    /// <summary>次遷移するシーンの名前</summary>
    [SerializeField, Header("プレイヤー強化画面")] SceneName _nextSceneName;
    [SerializeField,Header("次ステージの名前")] SceneName _nextStage;
    GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        _gameManager.NextSceneName = _nextSceneName.ToString();
        Resources.Load<PlayerData>("PlayerData").NextScene = _nextStage.ToString();
        //現在のシーンがゲームシーンだった時
        if (_stageNumber != GameManager.StageNumber.None)
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

    enum SceneName
    {
        Title,
        Clear,
        PlayerStrengthen,
        Stage1Easy,
        Stage2Easy,
        Stage3Easy,
        BossEasy,
        Stage1Normal, 
        Stage2Normal,
        Stage3Normal,
        BossNormal,
        Stage1Hard,
        Stage2Hard,
        Stage3Hard,
        BossHard
    } 
}
