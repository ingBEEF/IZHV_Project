using UnityEngine;

public class BossController : MonoBehaviour
{
    public Transform player;
    public Transform projectileSpawner;
    public GameObject projectilePrefab;
    
    public float moveSpeed = 10f;
    public float rotSpeed = 5f;
    public float gravity = -9.81f;
    private float verticalVelocity = 0f;
    public float attackRange = 30f;
    public float attackCooldown = 0.8f;
    
    private CharacterController controller;
    private float lastAttackTime;
    
    void Start()
    {
        controller = GetComponent<CharacterController>();    
    }
    
    void Update()
    {
        if (player == null) return;

        bool isGrounded = controller.isGrounded;
        if (isGrounded && verticalVelocity < 0)
        {
            verticalVelocity = -2f;
        }
        
        FacePlayer();
        
        float distance = Vector3.Distance(transform.position, player.position);
        if (distance > attackRange)
        {
            MoveTowardsPlayer();
        }
        else
        {
            Attack();
        }
        
        Vector3 gravityMove = new Vector3(0, verticalVelocity, 0);
        controller.Move(gravityMove * Time.deltaTime);
        verticalVelocity += gravity * Time.deltaTime;
    }

    void FacePlayer()
    {
        Vector3 direction = player.position - transform.position;
        direction.y = 0;

        if (direction != Vector3.zero)
        {
            Quaternion lookRotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.LookRotation(direction);
            transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotSpeed);
        }
    }

    void MoveTowardsPlayer()
    {
        Vector3 moveDirection = transform.forward * moveSpeed * Time.deltaTime;
        controller.Move(new Vector3(moveDirection.x, 0f, moveDirection.z));
    }

    void Attack()
    {
        if (Time.time - lastAttackTime < attackCooldown) return;
        
        lastAttackTime = Time.time;

        ShootProjectile();
    }

    void ShootProjectile()
    {
        if (projectilePrefab == null || projectileSpawner == null || player == null)
            return;

        Vector3 shootDir = (player.position) - projectileSpawner.position;
        shootDir.Normalize();

        GameObject proj = Instantiate(projectilePrefab, projectileSpawner.position, Quaternion.LookRotation(shootDir));

        Rigidbody rb = proj.GetComponent<Rigidbody>();
        if (rb != null)
        {
            rb.velocity = shootDir * 15f;
        }

        Destroy(proj, 5f);
    }

}
