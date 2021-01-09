using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DontDestroy : MonoBehaviour
{
    public GameObject clientManager;
    void Awake()
    {
        DontDestroyOnLoad(clientManager);
    }
}
