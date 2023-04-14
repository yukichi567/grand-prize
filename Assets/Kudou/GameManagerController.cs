using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManagerController : MonoBehaviour
{
    /// <summary>���݂̃Q�[���X�e�[�W�V�[��</summary>
    [SerializeField,Header("���݂̃Q�[���X�e�[�W�V�[��")]GameManager.StageNumber _stageNumber;
    GameManager _gameManager;
    [SerializeField, Header("��Փx�����@�G��HP�������グ�邩")] int _maxhpUp;
    [SerializeField, Header("��Փx�����@�G�̍U���͂������グ�邩")] int _attackPowerUp;
    EnemyBase[] _enemyBasise;
    void Start()
    {
        _gameManager = GameManager.Instance;
        _gameManager.stageNumber = _stageNumber;
        //���݂̃V�[�����Q�[���V�[����������
        if(_stageNumber != GameManager.StageNumber.None)
        {
            //��Ԃ��Q�[�����ɕύX
            _gameManager.gameState = GameManager.GameState.Game;
            DifficultyAdjustment();
        }
        else
        {
            //��Ԃ��^�C�g���E��Փx�I���E���̑��V�[����ԂɕύX
            _gameManager.gameState = GameManager.GameState.TitleOrDifficultySelect;
        }
    }

    /// <summary>��Փx����</summary>
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

    /// <summary>Enemy�̍ő�lHP�ƍU���͂��グ��֐�</summary>
    /// <param name="multiple">�{��(_maxhp�����o�[�ϐ��ɉ��{������Enemy��HP�ɑ�����)</param>
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
