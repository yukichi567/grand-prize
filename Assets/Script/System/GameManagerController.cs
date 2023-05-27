using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>���݂̃Q�[���X�e�[�W�V�[��</summary>
    [SerializeField, Header("���݂̃Q�[���X�e�[�W�V�[��")] GameManager.StageNumber _stageNumber;
    /// <summary>���J�ڂ���V�[���̖��O</summary>
    [SerializeField, Header("���J�ڂ���V�[���̖��O")] string _nextSceneName;
    [SerializeField] string[] _sceneNames;
    [SerializeField] SceneName _nextScene;
    GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        _gameManager.NextSceneName = _nextSceneName;
        ScenSquare.Instance.SceneName = _sceneNames[(int)_nextScene];
        //���݂̃V�[�����Q�[���V�[����������
        if (_stageNumber != GameManager.StageNumber.None)
        {
            //��Ԃ��Q�[�����ɕύX
            _gameManager.gameState = GameManager.GameState.Game;
            //��Փx�ɂ���Ă�EnemyHP�ő�l�ƍU���͂𒲐�
            _gameManager.DifficultyAdjustment();
        }
        else
        {
            //��Ԃ��^�C�g���E��Փx�I���E���̑��V�[����ԂɕύX
            _gameManager.gameState = GameManager.GameState.TitleOrDifficultySelect;
        }
    }

    enum SceneName
    {
        Title,
        Stage1,
        Stage2,
        Stage3,
        Boss,
        Clear,
        GameOver,
        PlayerStrengthen,
    }
    
}
