using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon_Fire : MonoBehaviour
{
    [SerializeField] private bool projectile_Shot;
     private Rigidbody2D rb;
    [SerializeField] private Transform bullet_Spawn_Point;
    [SerializeField] private GameObject bullet_Prefab;
    [SerializeField] private float bullet_Speed = 10f;
   
    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    private void Update()
    {
        //shooting code goes here 
    }

    private void Shoot_Projectile()
    {
        var bullet = Instantiate(bullet_Prefab, bullet_Spawn_Point.position, bullet_Spawn_Point.rotation);
        bullet.GetComponent<Rigidbody2D>().velocity = bullet_Spawn_Point.up * bullet_Speed;
    }
}
