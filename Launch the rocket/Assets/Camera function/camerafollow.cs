using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class camerafollow : MonoBehaviour
{
    private Transform playerTransform;
    // Start is called before the first frame update
    void Start()
    {
      playerTransform = GameObject.FindWithTag("LocalPlayer").transform;

    }

    // Update is called once per frame
    void LateUpdate()
    {
      Vector3 temp = transform.position;
      temp.x = playerTransform.position.x;
      temp.y = playerTransform.position.y;
      transform.position = temp;

      //fill in the border we want for the camera view
      transform.position = new Vector3(
      Mathf.Clamp(transform.position.x,-13f,13f),
      Mathf.Clamp(transform.position.y,-11.5f,11.5f),
      transform.position.z
      );
    }
}
