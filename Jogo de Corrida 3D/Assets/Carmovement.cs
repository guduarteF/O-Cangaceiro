using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Carmovement : MonoBehaviour
{
    private float dir = 0,speed = 0.5f;
   
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Rigidbody rb = GetComponent<Rigidbody>();
        if(Input.GetKey("up"))
        {
            dir = 0;
            if(rb.velocity.magnitude < 5f)
            {
                rb.AddForce(Vector3.forward * speed, ForceMode.Impulse);
            }
          
        }
        if(Input.GetKey("right"))
        {
            dir = 90;
            if(rb.velocity.magnitude < 5f)
            {
                rb.AddForce(Vector3.right * speed, ForceMode.Impulse);
            }
        }
        if(Input.GetKey("left"))
        {
            if(rb.velocity.magnitude < 5f)
            {
                dir = -90;
                rb.AddForce(Vector3.left * speed, ForceMode.Impulse);
            }
            
        }

        transform.rotation = Quaternion.Lerp(transform.rotation, Quaternion.Euler(0, dir, 0), Time.deltaTime * 2f);
    }
}
