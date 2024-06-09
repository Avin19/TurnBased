
using UnityEngine;

public class BulletProjectile : MonoBehaviour
{
    private Vector3 targetPosition;
    [SerializeField] private TrailRenderer trailRenderer;
    [SerializeField] private Transform pfBulletHitVfx;
    public void Setup(Vector3 targetPosition)
    {
        this.targetPosition = targetPosition;
    }
    private void Update()
    {
        Vector3 movDir = (targetPosition - transform.position).normalized;
        float moveSpeed = 200f;
        float distaceBeforeMove = Vector3.Distance(targetPosition, transform.position);
        transform.position += movDir * moveSpeed * Time.deltaTime;
        float distaceAfterMove = Vector3.Distance(targetPosition, transform.position);

        if (distaceBeforeMove < distaceAfterMove)
        {
            transform.position = targetPosition;
            trailRenderer.transform.parent = null;
            Destroy(gameObject);
            Instantiate(pfBulletHitVfx, targetPosition, Quaternion.identity);
        }



    }
}
