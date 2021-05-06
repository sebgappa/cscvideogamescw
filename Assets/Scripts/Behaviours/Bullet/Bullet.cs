using UnityEngine;

public class Bullet : MonoBehaviour
{
    public GameObject explosionEffect;

    [SerializeField]
    private float _bulletDamage = 20;

    public void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<Player>().TakeDamage(_bulletDamage);
            GameObject effect = Instantiate(explosionEffect, transform.position, Quaternion.identity);
            Destroy(gameObject);
            Destroy(effect, 0.5f);
        }
        Destroy(gameObject, 3f);
    }

    public void IncreaseStrength(float factor)
    {
        _bulletDamage *= factor;
    }

    public void ResetStrength(float factor)
    {
        _bulletDamage /= factor;
    }
}
