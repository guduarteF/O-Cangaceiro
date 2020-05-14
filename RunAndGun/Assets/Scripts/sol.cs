using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sol : MonoBehaviour
{
    public float posY = 18f;
    void Start()
    {
        
    }
    void Update()
    {
        transform.position = new Vector3(transform.position.x, posY, transform.position.z);
    }
}
