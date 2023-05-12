using UnityEngine;

public class PlayerAttack : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.GetComponent<EnemyBase>())
        {
            collision.gameObject.GetComponent<EnemyBase>().Damage(5);
        }
        //if (collision.CompareTag("Enemy"))
        //{
        //    collision.gameObject.GetComponent<EnemyBase>().Damage();
        //}
    }
}