using System.Collections;
using UnityEngine;

public class Cannon_Move : MonoBehaviour
{
    //[SerializeField] private Fire_Projectile weapon;
    [SerializeField] private Transform player;  // Rename 'target' to 'player'
    [SerializeField] private Transform barrel;
    [SerializeField] private float fireRate = 2f;  // Adjust the fire rate as needed
    private float nextFireTime;

    private void Update()
    {
        UpdateTargetPosition();  // New method to update player position
        Aim();

        // Check if it's time to fire
        if (Time.time >= nextFireTime)
        {
            //weapon.Fire();
            nextFireTime = Time.time + 1f / fireRate;  // Update next fire time
        }
    }

    private void UpdateTargetPosition()
    {
        // You can update the player position here or retrieve it from another source
        // For example, if the player has a tag "Player", you can find it dynamically:
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
    }

    private void Aim()
    {
        if (player == null)
            return;  // Skip aiming if player is not found

        float targetPlaneAngle = Vector3AngleOnPlane(player.position, transform.position, -transform.up, transform.forward);
        Vector3 newRotation = new Vector3(0, targetPlaneAngle, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler(newRotation), Time.deltaTime * 5f);  // Smooth rotation

        float upAngle = Vector3.Angle(player.position - barrel.position, barrel.transform.up);
        Vector3 upRotation = new Vector3(-upAngle + 90, 0, 0);
        barrel.rotation = Quaternion.Euler(upRotation);
    }

    private float Vector3AngleOnPlane(Vector3 from, Vector3 to, Vector3 planeNormal, Vector3 toZeroAngle)
    {
        Vector3 projectedVector = Vector3.ProjectOnPlane(from - to, planeNormal);
        float projectedVectorAngle = Vector3.SignedAngle(projectedVector, toZeroAngle, planeNormal);
        return projectedVectorAngle;
    }
}