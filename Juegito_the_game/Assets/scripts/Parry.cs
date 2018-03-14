﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Parry : MonoBehaviour {

    public GameObject bulletPrefab;
    public string playerNumber;
    public string player;
    public Vector2 vec2;
    public Vector2 DirectionJoyL = Vector2.zero;

    private bool inTrigger;
    private int parryPoints = 20;
    private GameObject playerObject;
    private Collider2D other;

    private void Awake()
    {
        playerObject = gameObject;
    }

    private void Update()
    {
        DirectionJoyL.x = Input.GetAxis(playerNumber + "LeftJoyX");
        DirectionJoyL.y = Input.GetAxis(playerNumber + "LeftJoyY");
        DirectionJoyL.Normalize();

        if (inTrigger == true)
        {
            if (Input.GetButtonDown(playerNumber + "XboxX"))
            {
                if (other.tag == "Bullet")
                {
                    Debug.Log("ActualParry!");

                    Destroy(other.gameObject);
                    inTrigger = false;

                    if (DirectionJoyL == new Vector2(0, 0))
                        DirectionJoyL = new Vector2(GameObject.Find(player).GetComponent<Movement>().side, 0);

                    SpawnBullet();
                }
            }
        }

    }

    void SpawnBullet()
    {
        playerObject.GetComponent<Shoot>().ultCharge += parryPoints;
        GameObject bullet = (GameObject)Instantiate(bulletPrefab, transform.position + new Vector3(0.4f * GameObject.Find(player).GetComponent<Movement>().side, 0, 0), transform.rotation);
        Destroy(bullet, 3);//Cambiar on collision con los bordes del mapa

    }

    void OnTriggerEnter2D(Collider2D other2)
    { 
        
        inTrigger = true;
        other = other2;


    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        inTrigger = false;
    }

}
