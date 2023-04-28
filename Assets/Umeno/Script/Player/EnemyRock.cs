using UnityEngine;

public class EnemyRock : MonoBehaviour
{
    [SerializeField] GameObject _cursor;
    Vector3 _enemyPosition;

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.CompareTag("Enemy"))
        {
            _enemyPosition = collision.gameObject.transform.position;
            PlayerController.Instance.EnemyPosition = _enemyPosition;
            _cursor.transform.position = _enemyPosition;
            _cursor.SetActive(true);
            PlayerController.Instance.IsEnemyRock = true;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        _cursor.SetActive(false);
        PlayerController.Instance.IsEnemyRock = false;
    }
}