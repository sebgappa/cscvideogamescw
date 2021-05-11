using UnityEngine;

public class BladeSaw : MonoBehaviour
{
    [SerializeField]
    private float _attackRate = 2f;
    private readonly int _damage = 10;
    private float _nextAttackTime = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= _nextAttackTime)
            {
                collision.gameObject.GetComponent<Player>().TakeDamage(_damage);
                _nextAttackTime = Time.time + 1 / _attackRate;
            }
        }
    }
}
