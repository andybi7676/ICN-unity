using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class cameraZoom : MonoBehaviour
{
    private GameObject bg;
    // Start is called before the first frame update
    void Start()
    {//background spriterenderer holder object
        bg = GameObject.Find("BG_Space");

        StartCoroutine(ZoomIn());
    }

    IEnumerator ZoomIn()
    {
        //zoom out instantly
        Camera.main.orthographicSize = 100;
        bg.transform.localScale = new Vector3(20, 20, 1);

        //calc bg zoom step
        float zs = (float)(20-1) / (float)(100-5);
        //calc: (init spriterenderer transform size - target size) / (init orthographicSize - target orthographicSize)
        //zoom in smoothly
        while (Camera.main.orthographicSize > 5)
        { yield return new WaitForSeconds(0.01f);
            Camera.main.orthographicSize -= 1f;

            // Widen the object by x, y, and z values
            bg.transform.localScale -= new Vector3(zs, zs, 0);
          //  Debug.Log("CameraZoom - ZoomIn, bg.transform.localScale:" + bg.transform.localScale);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
}
