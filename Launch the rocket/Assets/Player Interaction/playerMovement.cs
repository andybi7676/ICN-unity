using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerMovement : MonoBehaviour
{
    public GameObject gun;
    public Transform weaponSpawnPoint;

    Rigidbody2D rigidbody2d;
    public float speed = 3f;
    public Animator animator;
    float horizontal;
    float vertical;
    Vector2 movement;

    private bool isUsingWeapon = false;
    private GameObject currentWeapon;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Client.instance.myId!= transform.parent.GetComponent<PlayerManager>().id){
          return;
        }
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");
        animator.SetFloat("Horizontal", movement.x);
        animator.SetFloat("Vertical", movement.y);

        // Press G to take out or put back weapon
        if (!isUsingWeapon) {
          if(Input.GetKeyDown(KeyCode.G)){
              currentWeapon = Instantiate(gun, weaponSpawnPoint.position, Quaternion.identity);
              currentWeapon.transform.parent = gameObject.transform;
              isUsingWeapon = true;
            }
        }
        else {
            if(Input.GetKeyDown(KeyCode.G)){
              Destroy(currentWeapon);
              isUsingWeapon = false;
            }
        }
    }

    // void FixedUpdate()
    // {
    //     Vector3 position = rigidbody2d.position;
    //     position.x = position.x + 3.0f * horizontal * Time.deltaTime;
    //     position.y = position.y + 3.0f * vertical * Time.deltaTime;
    //     position.z = 0;
    //     position = new Vector3(
    //       Mathf.Clamp(position.x,-22f,21.5f),
    //       Mathf.Clamp(position.y,-16f,15.5f),
    //       0
    //     );
    //
    //     rigidbody2d.MovePosition(position);
    // }

    void FixedUpdate(){
      if (Client.instance.myId!= transform.parent.GetComponent<PlayerManager>().id){
          return;
      }
      Vector2 position = rigidbody2d.position;
      position = rigidbody2d.position + movement * speed * Time.fixedDeltaTime;
      position = new Vector2(
      Mathf.Clamp(position.x,-22f,21.5f), Mathf.Clamp(position.y,-16f,15.5f));
      rigidbody2d.MovePosition(position);

      ClientSend.PlayerMovement(position);
    }
}
