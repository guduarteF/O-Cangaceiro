using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class parallaxY : MonoBehaviour
{
    private float lenght, startpos;
    public GameObject cam;
    public float parallaxEffect;
    void Start()
    {
        startpos = transform.position.x;
        lenght = GetComponent<SpriteRenderer>().bounds.size.x;

    }

    // Update is called once per frame
    void Update()
    {

        float dist = (cam.transform.position.x * parallaxEffect);
        float temp = (cam.transform.position.x * (1 - parallaxEffect));
        transform.position = new Vector3(startpos + dist, transform.position.y, transform.position.z);

        if (temp > startpos + lenght) startpos += lenght;
        else if (temp < startpos - lenght) startpos -= lenght;
    }
}
