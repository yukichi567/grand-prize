using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    [SerializeField, Header("��Փx�����@�G��HP�������グ�邩")] int _maxhpUp;
    [SerializeField, Header("��Փx�����@�G�̍U���͂������グ�邩")] int _attackPowerUp;
    EnemyBase[] _enemyBasise;
    /// <summary>�X�e�[�W�N���A�܂łɂ����������ԗp�ϐ�</summary>
    float _timer;
    /// <summary>Player�����̂��߂�Point�ϐ�</summary>
    int _point;
    /// <summary>�e�X�e�[�W�̃N���A���Ԃ̍ŏ��l��ۑ�����p��Dictinary</summary>
    Dictionary<StageNumber, float> _gameLowerTime = new Dictionary<StageNumber, float>();
    /// <summary>���݂̃Q�[�����</summary>
    GameState _gameState = GameState.Game;
    /// <summary>���݃v���C���Ă���X�e�[�W</summary>
    StageNumber _stageNumber = StageNumber.Stage1;
    /// <summary>���݃v���C���Ă����Փx</summary>
    StageDifficulty _stageDifficulty = StageDifficulty.Easy;
    public StageNumber stageNumber { get { return _stageNumber; } set { _stageNumber = value; } }
    public StageDifficulty stageDifficulty { get { return _stageDifficulty; } set { _stageDifficulty = value; } }
    public GameState gameState { get { return _gameState; } set { _gameState = value; } }
    
    public float Timer { get { return _timer; }}
    public int Point { get { return _point; }}
    private void Update()
    {
        //�Q�[������������
        if (gameState == GameState.Game)
        {
            _timer += Time.deltaTime;
            Debug.Log("Game��");
        }
        else
        {
            switch (_gameState)
            {
                //�Q�[���N���A����ɂȂ�����
                case GameState.GameClear:
                    LowerTimeSave();
                    TimerReset();
                    break;
                //�Q�[���I�[�o�[����ɂȂ�����
                case GameState.GameOver:
                    TimerReset();
                    break;
            }
        }
    }
    
�@�@/// <summary>�|�C���g���Z�֐�</summary>
  �@/// <param name="pointNum">���Z����|�C���g��</param>
    public void PointPlus(int pointNum)
    {
        _point += pointNum;
    }
    /// <summary>�|�C���g���Z�֐�</summary>
    /// <param name="pointNum">���Z����|�C���g��</param>
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
    
    /// <summary>�X�e�[�W�N���A���Ԃ̍X�V(�N���A���Ԃ��O��N���A�������Ԃ��Z��������)</summary>
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
    /// �n�C�X�R�A(���v���C���Ă���X�e�[�W�̃N���A�̍ŒZ����)��Ԃ��֐�
    /// </summary>
    /// <returns>���߂ẴX�e�[�W��������O��Ԃ��E���ڈȏ�o�����Ă���X�e�[�W�������炻�̃X�e�[�W�̃N���A�̍ŒZ���Ԃ�������</returns>
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
    /// <summary>�N���A����Timer��0�ɂ���֐�</summary>
    public void TimerReset()
    {
        _timer = 0;
    }
    public void StageDifficultySelect(int difficultyNumber)
    {
        if (difficultyNumber > 2 || difficultyNumber < 0)
        {
            Debug.Log("�w�肳�ꂽ��Փx�͑��݂��܂���O����Q�̐����������ɓ���Ă�������" +
                    "Easy�͂O�@Normal�͂P�@Hard�͂Q�@�ł�");
        }
        else
        {
            _stageDifficulty = (StageDifficulty)difficultyNumber;
        }
    }
    /// <summary>�X�e�[�W�̓�Փx�Ǘ��p��enum</summary>
    public enum StageDifficulty
    {
        Easy,
        Normal,
        Hard,
    }
    /// <summary>�Q�[���̏�ԊǗ��p��enum</summary>
    public enum GameState
    {
        /// <summary>�^�C�g���V�[���E��Փx�I���V�[��</summary>
        TitleOrDifficultySelect,
        /// <summary>�Q�[����</summary>
        Game,
        /// <summary>�Q�[���I�[�o�[</summary>
        GameOver,
        /// <summary>�Q�[���N���A</summary>
        GameClear,
    }
    /// <summary>���݃v���C���Ă���Q�[���̃X�e�[�WNumber�Ǘ��penum</summary>
    public enum StageNumber
    {
        /// <summary> �Q�[���V�[���ł͂Ȃ��Ƃ�(�^�C�g���E��Փx�I��)</summary>
        None,
        Stage1,
        Stage2,
        Stage3,
    }

    /// <summary>��Փx����</summary>
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

    /// <summary>Enemy�̍ő�lHP�ƍU���͂��グ��֐�</summary>
    /// <param name="multiple">�{��(_maxhp�����o�[�ϐ��ɉ��{������Enemy��HP�ɑ�����)</param>
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
