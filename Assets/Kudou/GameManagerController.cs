using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>現在のゲームステージシーン</summary>
    [SerializeField,Header("現在のゲームステージシーン")]GameManager.StageNumber _stageNumber;
    GameManager _gameManager;
    [SerializeField, Header("難易度調整　敵のHPをいくつ上げるか")] int _maxhpUp;
    [SerializeField, Header("難易度調整　敵の攻撃力をいくつ上げるか")] int _attackPowerUp;
    EnemyBase[] _enemyBasise;
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        //現在のシーンがゲームシーンだった時
        if(_stageNumber != GameManager.StageNumber.None)
        {
            //状態をゲーム中に変更
            _gameManager.gameState = GameManager.GameState.Game;
            DifficultyAdjustment();
        }
        else
        {
            //状態をタイトル・難易度選択・その他シーン状態に変更
            _gameManager.gameState = GameManager.GameState.TitleOrDifficultySelect;
        }
    }

    /// <summary>難易度調整</summary>
    void DifficultyAdjustment()
    {
        switch(_gameManager.stageDifficulty)
        {
            case GameManager.StageDifficulty.Easy:
                break;
            case GameManager.StageDifficulty.Normal:
                EnemyHPAttackPowerUp(1);
                break;
            case GameManager.StageDifficulty.Hard:
                EnemyHPAttackPowerUp(2);
                break;
        }
    }

    /// <summary>Enemyの最大値HPと攻撃力を上げる関数</summary>
    /// <param name="multiple">倍数(_maxhpメンバー変数に何倍かけてEnemyのHPに足すか)</param>
    void EnemyHPAttackPowerUp(int multiple)
    {
        _enemyBasise = FindObjectsOfType<EnemyBase>();
        for(var i = 0; i < _enemyBasise.Length; i++)
        {
            _enemyBasise[i].MaxHPUP(_maxhpUp * multiple);
            _enemyBasise[i].AttackPowerUp(_attackPowerUp * multiple);
        }
    }

}
