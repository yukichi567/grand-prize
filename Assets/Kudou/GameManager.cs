using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField, Header("難易度調整　敵のHPをいくつ上げるか")] int _maxhpUp;
    [SerializeField, Header("難易度調整　敵の攻撃力をいくつ上げるか")] int _attackPowerUp;
    EnemyBase[] _enemyBasise;
    /// <summary>ステージクリアまでにかかった時間用変数</summary>
    float _timer;
    /// <summary>Player強化のためのPoint変数</summary>
    int _point;
    /// <summary>各ステージのクリア時間の最小値を保存する用のDictinary</summary>
    Dictionary<StageNumber, float> _gameLowerTime = new Dictionary<StageNumber, float>();
    /// <summary>現在のゲーム状態</summary>
    GameState _gameState = GameState.Game;
    /// <summary>現在プレイしているステージ</summary>
    StageNumber _stageNumber = StageNumber.Stage1;
    /// <summary>現在プレイしている難易度</summary>
    StageDifficulty _stageDifficulty = StageDifficulty.Easy;
    public StageNumber stageNumber { get { return _stageNumber; } set { _stageNumber = value; } }
    public StageDifficulty stageDifficulty { get { return _stageDifficulty; } set { _stageDifficulty = value; } }
    public GameState gameState { get { return _gameState; } set { _gameState = value; } }
    
    public float Timer { get { return _timer; }}
    public int Point { get { return _point; }}
    private void Update()
    {
        //ゲーム中だったら
        if (gameState == GameState.Game)
        {
            _timer += Time.deltaTime;
            Debug.Log("Game中");
        }
        else
        {
            switch (_gameState)
            {
                //ゲームクリア判定になったら
                case GameState.GameClear:
                    LowerTimeSave();
                    TimerReset();
                    break;
                //ゲームオーバー判定になったら
                case GameState.GameOver:
                    TimerReset();
                    break;
            }
        }
    }
    
　　/// <summary>ポイント加算関数</summary>
  　/// <param name="pointNum">加算するポイント数</param>
    public void PointPlus(int pointNum)
    {
        _point += pointNum;
    }
    /// <summary>ポイント減算関数</summary>
    /// <param name="pointNum">減算するポイント数</param>
    public bool PointMinus(int pointNum)
    {
        if (pointNum <= _point)
        {
            _point -= pointNum;
            return true;
        }
        else
        {
            return false;
        }
    } 
    
    /// <summary>ステージクリア時間の更新(クリア時間が前回クリアした時間より短かったら)</summary>
    public void LowerTimeSave()
    {
        if(_gameLowerTime.ContainsKey(_stageNumber))
        {
            if(_gameLowerTime[_stageNumber] > _timer)
            {
                _gameLowerTime[_stageNumber] = _timer;
            }
        }
        else
        {
            _gameLowerTime.Add(_stageNumber, _timer);
        }
    }

    /// <summary>
    /// ハイスコア(今プレイしているステージのクリアの最短時間)を返す関数
    /// </summary>
    /// <returns>初めてのステージだったら０を返す・二回目以上経験しているステージだったらそのステージのクリアの最短時間をかえす</returns>
    public float LowerTimeLoad()
    {
        if (_gameLowerTime.ContainsKey(_stageNumber))
        {
            return _gameLowerTime[_stageNumber];
        }
        else
        {
            return 0;
        }
    }
    /// <summary>クリア時にTimerを0にする関数</summary>
    public void TimerReset()
    {
        _timer = 0;
    }
    public void StageDifficultySelect(int difficultyNumber)
    {
        if (difficultyNumber > 2 || difficultyNumber < 0)
        {
            Debug.Log("指定された難易度は存在しません０から２の数字を引数に入れてください" +
                    "Easyは０　Normalは１　Hardは２　です");
        }
        else
        {
            _stageDifficulty = (StageDifficulty)difficultyNumber;
        }
    }
    /// <summary>ステージの難易度管理用のenum</summary>
    public enum StageDifficulty
    {
        Easy,
        Normal,
        Hard,
    }
    /// <summary>ゲームの状態管理用のenum</summary>
    public enum GameState
    {
        /// <summary>タイトルシーン・難易度選択シーン</summary>
        TitleOrDifficultySelect,
        /// <summary>ゲーム中</summary>
        Game,
        /// <summary>ゲームオーバー</summary>
        GameOver,
        /// <summary>ゲームクリア</summary>
        GameClear,
    }
    /// <summary>現在プレイしているゲームのステージNumber管理用enum</summary>
    public enum StageNumber
    {
        /// <summary> ゲームシーンではないとき(タイトル・難易度選択)</summary>
        None,
        Stage1,
        Stage2,
        Stage3,
    }

    /// <summary>難易度調整</summary>
    public  void DifficultyAdjustment()
    {
        switch (stageDifficulty)
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
        for (var i = 0; i < _enemyBasise.Length; i++)
        {
            _enemyBasise[i].MaxHPUP(_maxhpUp * multiple);
            _enemyBasise[i].AttackPowerUp(_attackPowerUp * multiple);
        }
    }

}
