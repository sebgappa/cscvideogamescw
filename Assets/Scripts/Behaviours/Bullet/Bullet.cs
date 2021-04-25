using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffect;
    public int bulletDamage = 20;
    void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(bulletDamage);
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 0.5f);
        }
        Destroy(gameObject, 3f);
    }
}
