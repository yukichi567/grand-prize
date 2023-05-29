using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>���݂̃Q�[���X�e�[�W�V�[��</summary>
    [SerializeField, Header("���݂̃Q�[���X�e�[�W�V�[��")] GameManager.StageNumber _stageNumber;
    /// <summary>���J�ڂ���V�[���̖��O</summary>
    [SerializeField, Header("�v���C���[�������")] SceneName _nextSceneName;
    [SerializeField,Header("���X�e�[�W�̖��O")] SceneName _nextStage;
    GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        _gameManager.NextSceneName = _nextSceneName.ToString();
        Resources.Load<PlayerData>("PlayerData").NextScene = _nextStage.ToString();
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
