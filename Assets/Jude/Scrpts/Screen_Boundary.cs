using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Screen_Boundary : MonoBehaviour
{
    [SerializeField] private float leftBoarder;
    [SerializeField] private float rightBoarder;
    [SerializeField] private float upBoarder;
    [SerializeField] private float downBoarder;

    private void Update()
    {
        float clampedX = Mathf.Clamp(transform.position.x, leftBoarder, rightBoarder);
        float clampedY = Mathf.Clamp(transform.position.y, upBoarder, downBoarder);

        transform.position = new Vector3(clampedX, clampedY, transform.position.z);
    }
}