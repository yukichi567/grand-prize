using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// ���̃N���X���p������ƃV���O���g�����ɂȂ�܂��B
/// </summary>
/// <typeparam name="T">�p�������N���X(�h���N���X)�̃N���X��</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            //�ǂݎ��̍ہAinstance���Ȃ�������
            if(instance == null)
            {
                Type t = typeof(T);
                //��������T�̃I�u�W�F�N�g�^���L���X�g����T�^�ɕϊ�
                instance = (T)FindObjectOfType(t);
                if (!instance)
                {
                    Debug.LogError(t + "���A�^�b�`���Ă���GameObject�͂���܂���");
                }
            }
            return instance;
        }
    }

    protected virtual void Awake()
    {
        if (FindObjectsOfType<T>().Length > 1)
        {
            Destroy(this);
        }
        else
        {
            DontDestroyOnLoad(this);
        }
    }
}
