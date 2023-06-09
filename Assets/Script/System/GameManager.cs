using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UniRx;
using System.Linq;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField, Header("難易度調整　敵のHPをいくつ上げるか")] int _maxhpUp;
    [SerializeField, Header("難易度調整　敵の攻撃力をいくつ上げるか")] int _attackPowerUp;
    [SerializeField, Header("何秒Timeをきったらpointボーナスか")] float _bonusPointTime;
    [SerializeField, Header("ボーナスポイント数")] int _bonusPoint;
    EnemyBase[] _enemyBasise;
    /// <summary>ステージクリアまでにかかった時間用変数</summary>
    float _timer;
    /// <summary>Player強化のためのPoint変数</summary>
    int _point = 10;
    /// <summary>各ステージのクリア時間の最小値を保存する用のDictinary</summary>
    Dictionary<StageNumber, float> _gameLowerTime = new Dictionary<StageNumber, float>();
    /// <summary>現在のゲーム状態</summary>
    GameState _gameState = GameState.Game;
    /// <summary>現在プレイしているステージ</summary>
    StageNumber _stageNumber = StageNumber.Stage1;
    /// <summary>現在プレイしている難易度</summary>
    StageDifficulty _stageDifficulty = StageDifficulty.Easy;
    /// <summary>次遷移するシーンの名前</summary>
    string _nextSceneName;
    ReactiveProperty<int> _power = new ReactiveProperty<int>(2);
    ReactiveProperty<int> _speed = new ReactiveProperty<int>(5);
    ReactiveProperty<float> _dushAttack = new ReactiveProperty<float>(10f);
    Player _playerScript;

    public ReactiveProperty<int> Power { get => _power; set => _power = value; }
    public ReactiveProperty<int> Speed { get => _speed; set => _speed = value; }
    public ReactiveProperty<float> DushAttack { get => _dushAttack; set => _dushAttack = value; }
    public StageNumber stageNumber { get { return _stageNumber; } set { _stageNumber = value; } }
    public StageDifficulty stageDifficulty { get { return _stageDifficulty; } set { _stageDifficulty = value; } }
    public GameState gameState { get { return _gameState; } set { _gameState = value; } }
    public string NextSceneName { get { return _nextSceneName; } set { _nextSceneName = value; } }
    
    public float Timer { get { return _timer; }}
    public int Point { get { return _point; }}
    private void Awake()
    {
        base.Awake();
        _playerScript = FindObjectOfType<Player>();
    }

    private void Start()
    {
        this.ObserveEveryValueChanged(x => x._power).Subscribe(newValue => _playerScript.Power = newValue.Value);
        this.ObserveEveryValueChanged(x => x._speed).Subscribe(newValue => _playerScript.Speed = newValue.Value);
        this.ObserveEveryValueChanged(x => x._dushAttack).Subscribe(newValue => _playerScript.DushAttack = newValue.Value);
    }
    private void Update()
    {
        _playerScript.Power = _power.Value;
        _playerScript.Speed = _speed.Value;
        _playerScript.DushAttack = _dushAttack.Value;
        //ゲーム中だったら
        Debug.Log($"Power = {_power}, Speed = {_speed}, DashPower = {_dushAttack}");
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
                    //次のシーンに遷移
                    Debug.Log("呼んだ?");
                    gameState = GameState.PlayerStrengthen;
                   FindObjectOfType<ScenSquare>().ScenChange(_nextSceneName);
                    //タイムアタック用のタイムを保存
                    LowerTimeSave();
                    TimerReset();
                    break;
                //ゲームオーバー判定になったら
                case GameState.GameOver:
                    //現在のシーンの再リロード
                    Resources.Load<PlayerData>("PlayerData").NextScene = SceneManager.GetActiveScene().name;
                    _gameState = GameState.Game;
                    FindObjectOfType<ScenSquare>().ScenChange("GameOver");
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
        //引数の値が０〜２だったら
        if (difficultyNumber >= 0 || difficultyNumber <= 2)
        {
　　　　　　//GameManagerのenum難易度に代入
            _stageDifficulty = (StageDifficulty)difficultyNumber;
        }
        else
        {
            Debug.Log("指定された難易度は存在しません０から２の数字を引数に入れてください" +
                    "Easyは０　Normalは１　Hardは２　です");
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
        PlayerStrengthen,
    }
    /// <summary>現在プレイしているゲームのステージNumber管理用enum</summary>
    public enum StageNumber
    {
        /// <summary> ゲームシーンではないとき(タイトル・難易度選択)</summary>
        None,
        Stage1,
        Stage2,
        Stage3,
        PlayerStrengthen,
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

    void TimerScorePoint(float timer)
    {
        if(timer < _bonusPointTime)
        {
            PointPlus(_bonusPoint);
        }
    }
}
