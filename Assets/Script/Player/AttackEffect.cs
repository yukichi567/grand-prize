using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] int _damege;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        EnemyBase enemyScript = collision.gameObject.GetComponent<EnemyBase>();
        if (enemyScript)
        {
            int playerPower = FindObjectOfType<Player>().Power.Value;
            enemyScript.Damage(playerPower * _damege);
            Debug.Log(playerPower * _damege);
        }
    }
}