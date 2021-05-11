using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffect;
    private float _bulletDamage;
    private float _effectDelay = 0.5f;
    private float _bulletDelay = 3f;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.GetComponent<Player>() != null)
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_bulletDamage);
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, _effectDelay);
        }
        Destroy(gameObject, _bulletDelay);
    }

    public void setDamage(float bulletDamage)
    {
        _bulletDamage = bulletDamage;
    }
}
