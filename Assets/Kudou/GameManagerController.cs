using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>���݂̃Q�[���X�e�[�W�V�[��</summary>
    [SerializeField,Header("���݂̃Q�[���X�e�[�W�V�[��")]GameManager.StageNumber _stageNumber;
    GameManager _gameManager;
    
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        //���݂̃V�[�����Q�[���V�[����������
        if(_stageNumber != GameManager.StageNumber.None)
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

    
}