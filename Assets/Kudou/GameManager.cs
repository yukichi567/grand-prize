using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : SingletonMonoBehaviour<GameManager>
{
    public void Singleton()
    {
        Debug.Log("シングルトン化！");
    }
}
