using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        int damage = GetComponentInParent<PlayerController>().Power.Value;
        if (collision.gameObject.GetComponent<EnemyBase>())
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(damage);
        }
    }
}