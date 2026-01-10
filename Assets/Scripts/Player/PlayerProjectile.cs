using UnityEngine;

public class PlayerProjectile : MonoBehaviour
{
    public float speed = 50f;
    private Vector3 targetPoint;

    public void SetTarget(Vector3 target)
    {
        targetPoint = target;
    }

    void Update()
    {
        if (targetPoint == Vector3.zero) return;

        Vector3 dir = (targetPoint - transform.position).normalized;
        float distance = Vector3.Distance(transform.position, targetPoint);

        float step = speed * Time.deltaTime;
        if (step >= distance)
        {
            transform.position = targetPoint;
            Destroy(gameObject);
        }
        else
        {
            transform.position += dir * step;
        }
    }
}
