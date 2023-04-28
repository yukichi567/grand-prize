using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

/// <summary>
/// このクラスを継承するとシングルトン化になります。
/// </summary>
/// <typeparam name="T">継承したクラス(派生クラス)のクラス名</typeparam>
public class SingletonMonoBehaviour<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T instance;
    public static T Instance
    {
        get
        {
            //読み取りの際、instanceがなかったら
            if(instance == null)
            {
                Type t = typeof(T);
                //検索したTのオブジェクト型をキャストしてT型に変換
                instance = (T)FindObjectOfType(t);
                if (!instance)
                {
                    Debug.LogError(t + "をアタッチしているGameObjectはありません");
                }
            }
            return instance;
        }
    }

    void Awake()
    {
        if(instance == null)
        {
            instance = this as T;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
