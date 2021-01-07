using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class move : MonoBehaviour
{
    private Rigidbody2D rb;
    private float dirX,dirY,moveSpeed;
    // Start is called before the first frame update
    void Start()
    {
      rb = GetComponent<Rigidbody2D>();
      moveSpeed = 5f;
    }

    // Update is called once per frame
    void Update()
    {
      dirX = Input.GetAxisRaw("Horizontal")*moveSpeed;
      dirY = Input.GetAxisRaw("Vertical")*moveSpeed;
      transform.position = new Vector2(
        Mathf.Clamp(transform.position.x,-10f,2.3f),
        Mathf.Clamp(transform.position.y,-6.6f,2.5f)
      );
    }
}
