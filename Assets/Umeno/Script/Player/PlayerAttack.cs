﻿using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.CompareTag("Enemy"))
        {
            //collision.gameObject.GetComponent<EnemyBase>().Damage();
        }
    }
}