using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameClearOverJuge : MonoBehaviour
{
    [SerializeField, Header("Game”»’è")] GameManager.GameState _state; 
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            GameManager.Instance.gameState = _state;
        }
    }
}
