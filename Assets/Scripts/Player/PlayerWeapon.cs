using UnityEngine;

public class PlayerWeapon : MonoBehaviour
{
    public Transform firePoint;
    public GameObject projectilePrefab;
    public Camera camera;
    public LayerMask hitLayers;

    public float fireRate = 0.1f;
    public int damage = 10;
    public float maxRange = 100f;

    private float lastFireTime;

    void Update()
    {
        if (Input.GetMouseButton(0))
        {
            TryShoot();
        }
    }

    void TryShoot()
    {
        if (Time.time - lastFireTime < fireRate) return;

        lastFireTime = Time.time;
        Shoot();
    }

    void Shoot()
    {
        Ray ray = camera.ViewportPointToRay(new Vector3(0.5f, 0.5f, 0f));
        Vector3 hitPoint = ray.GetPoint(maxRange);
        
        if (Physics.Raycast(ray, out RaycastHit hit, maxRange, hitLayers))
        {
            hitPoint = hit.point;
            Debug.Log("Hit: " + hit.collider.name);

            BossHealth bossHealth = hit.collider.GetComponentInParent<BossHealth>();
            if (bossHealth != null)
            {
                bossHealth.TakeDamage(damage);
                Debug.Log($"Hit boss: {hit.collider.name} | Damage: {damage}");
            }
        }

        if (projectilePrefab != null && firePoint != null)
        {
            GameObject proj = Instantiate(projectilePrefab, firePoint.position, firePoint.rotation);
            proj.transform.forward = (hitPoint - firePoint.position).normalized;

            PlayerProjectile visual = proj.GetComponent<PlayerProjectile>();
            if (visual != null)
            {
                visual.SetTarget(hitPoint);
            }
        }
    }
}
