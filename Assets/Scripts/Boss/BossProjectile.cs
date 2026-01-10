using UnityEngine;

public class BossProjectile : MonoBehaviour
{
    public float speed = 80f;
    public int damage = 10;
    public float lifetime = 5f;
    
    void Start()
    {
        Destroy(gameObject, lifetime);    
    }
    
    void Update()
    {
        // transform.Translate(Vector3.forward * speed * Time.deltaTime);    
        transform.position += transform.forward * speed * Time.deltaTime;
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            PlayerHealth playerHealth = other.GetComponentInParent<PlayerHealth>();
            if (playerHealth != null)
            {
                playerHealth.TakeDamage(damage);
            }
            Destroy(gameObject);
        }
        else if (!other.CompareTag("Boss"))
        {
            Destroy(gameObject);
        }
    }
}
