using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectToServer : MonoBehaviour
{
    // Start is called before the first frame update
    public static ConnectToServer instance;
    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else if (instance != this)
        {
            Debug.Log("Instance already exists, destroying object!");
            Destroy(this);
        }
    }

    void Start()
    {
        ConnectServer();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void ConnectServer()
    {
        //IPaddress.interactable = false;
        Client.instance.ConnectToServer();
    }
}
