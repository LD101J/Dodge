using UnityEngine;
using System.Collections;

public class turret_Script : MonoBehaviour
{
    [SerializeField] private float range;
    [SerializeField] private float fireRate;
    [SerializeField] private Transform target;
    [SerializeField] private bool targetDetected = false;
    [SerializeField] private bool isShooting = false; // Flag to indicate whether the turret is currently shooting
    [SerializeField] private GameObject radiusDetector;
    [SerializeField] private Transform cannon; // Reference to the cannon's transform
    [SerializeField] private GameObject bullet;
    [SerializeField] private Transform firePoint;
    [SerializeField] private float bulletSpeed;
    private Vector2 direction;

    private void Update()
    {
        if (target != null)
        {
            Vector2 targetPos = target.position;
            direction = targetPos - (Vector2)transform.position;
            RaycastHit2D rayInfo = Physics2D.Raycast(transform.position, direction, range);
            if (rayInfo.collider != null && rayInfo.collider.CompareTag("Player"))
            {
                if (!targetDetected)
                {
                    targetDetected = true;
                    radiusDetector.GetComponent<SpriteRenderer>().color = Color.red;
                    if (!isShooting) // If not already shooting, start shooting
                    {
                        StartCoroutine(ShootCoroutine());
                    }
                }
            }
            else
            {
                if (targetDetected)
                {
                    targetDetected = false;
                    radiusDetector.GetComponent<SpriteRenderer>().color = Color.white;
                    // No need to stop the coroutine here
                }
            }

            if (targetDetected)
            {
                // Calculate the angle to rotate the cannon towards the player
                float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg + 80.3f;

                // Create a quaternion rotation based on the calculated angle
                Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);

                // Rotate the cannon towards the player
                cannon.rotation = rotation;
            }
        }
    }

    private IEnumerator ShootCoroutine()
    {
        if (!isShooting)
        {
            isShooting = true;
            ShootProjectile();
            yield return new WaitForSeconds(1 / fireRate);
            isShooting = false;
        }
    }

    private void ShootProjectile()
    {
        GameObject bulletInstance = Instantiate(bullet, firePoint.position, Quaternion.identity);
        Rigidbody2D rb = bulletInstance.GetComponent<Rigidbody2D>();
        if (rb != null)
        {
            rb.AddForce(direction.normalized * bulletSpeed, ForceMode2D.Impulse);
        }
        else
        {
            Debug.LogError("Bullet prefab does not have Rigidbody2D component attached!");
        }
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.DrawWireSphere(transform.position, range);
    }
}