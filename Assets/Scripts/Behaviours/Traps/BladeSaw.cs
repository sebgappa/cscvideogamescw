using UnityEngine;

public class BladeSaw : MonoBehaviour
{
    private readonly int damage = 10;
    public float attackRate = 2f;
    private float nextAttackTime = 0;

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            if (Time.time >= nextAttackTime)
            {
                Debug.Log("Here!");
                collision.gameObject.GetComponent<Player>().TakeDamage(damage);
                nextAttackTime = Time.time + 1 / attackRate;
            }
        }
    }
}
