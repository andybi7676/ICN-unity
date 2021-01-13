using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour {

    public float offset;

    public GameObject projectile;
    public GameObject shotEffect;
    public Transform shotPoint;
    // public GameObject Player;
    // public Animator camAnim;

    private float timeBtwShots;
    public static float startTimeBtwShots = (float)1.8;

    private void Update()
    {
        if (Client.instance.myId!= transform.parent.parent.GetComponent<PlayerManager>().id){
          return;
        }
        // Handles the weapon facing direction
        // facingDirection facingDirection = Player.GetComponent<facingDirection>();
        // if (facingDirection.facing == "left")
        // {
        //     transform.Rotate(0, 0, 0);
        // }
        // else
        // {
        //     transform.Rotate(0, 180, 0);
        // }

        // Handles the weapon rotation
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ + offset);
        ClientSend.GunRotation(transform.rotation);

        if (timeBtwShots <= 0)
        {
            if (Input.GetMouseButton(0))
            {
                Instantiate(shotEffect, shotPoint.position, Quaternion.identity);
                // camAnim.SetTrigger("shake");
                Instantiate(projectile, shotPoint.position, transform.rotation);
                ClientSend.SpawnBullet(shotPoint.position, transform.rotation);
                timeBtwShots = startTimeBtwShots;
            }
        }
        else {
            timeBtwShots -= Time.deltaTime;
        }
       
    }
}