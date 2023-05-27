using UnityEngine;

public class AttackEffect : MonoBehaviour
{
    [SerializeField] int _damege;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBase>())
        {
            int playerPower = GetComponentInParent<Player>().Power.Value;
            collision.gameObject.GetComponent<EnemyBase>().Damage(playerPower * _damege);
        }
    }
}