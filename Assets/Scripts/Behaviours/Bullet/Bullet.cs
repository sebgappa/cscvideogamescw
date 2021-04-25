using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffect;
    public int bulletDamage = 20;
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);

        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
        }
        Destroy(gameObject);
        Destroy(effect, 0.5f);
    }
}
