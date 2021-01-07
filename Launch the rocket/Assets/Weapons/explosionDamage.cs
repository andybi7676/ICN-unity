using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class explosionDamage : MonoBehaviour
{
    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    void OnTriggerEnter2D(Collider2D collider){
        if (collider.tag == "LocalPlayer") {
            collider.gameObject.GetComponent<droppingBomb> ().getBombed ();
            bullet.GetComponent<Projectile> ().DestroyProjectile();
        }
    }
}
