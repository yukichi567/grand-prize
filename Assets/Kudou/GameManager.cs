using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
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
    public StageNumber stageNumber { get { return _stageNumber; } set { _stageNumber = value; } }
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
    public void PointMinus(int pointNum)
    {
        if (pointNum <= _point)
        {
            _point -= pointNum;
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

    /// <summary>�X�e�[�W�̓�Փx�Ǘ��p��enum</summary>
    enum StageDifficulty
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
}
