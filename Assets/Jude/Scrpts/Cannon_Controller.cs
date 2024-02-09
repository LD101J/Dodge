using UnityEngine;

public class Cannon_Controller : MonoBehaviour
{
    public Transform player; // Reference to the player GameObject
    public float rotationSpeed = 5f; // Speed at which the cannon rotates

    // Update is called once per frame
    void Update()
    {
        if (player != null)
        {
            // Calculate the direction from the cannon to the player
            Vector3 direction = player.position - transform.position;
            direction.Normalize();

            // Calculate the angle to rotate the cannon
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;

            // Rotate the cannon towards the player smoothly
            Quaternion rotation = Quaternion.AngleAxis(angle, Vector3.forward);
            transform.rotation = Quaternion.Slerp(transform.rotation, rotation, rotationSpeed * Time.deltaTime);
        }
    }
}