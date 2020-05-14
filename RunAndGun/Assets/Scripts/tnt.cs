using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class tnt : MonoBehaviour
{

    public float force;
    public ParticleSystem explosionpart;
    public bool explosao;
    public static tnt tntscript;
    // Start is called before the first frame update
    void Start()
    {
        tntscript = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.gameObject.CompareTag("balaPlayer"))
        {
            FindObjectOfType<soundManager>().Play("barrel_hit");
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.AddForce(Vector2.right * force,0);
            GetComponent<Animator>().SetBool("barrelhit", true);
            StartCoroutine(Explosion());
            
           
        }
        
    }
 
    IEnumerator Explosion()
    {
        yield return new WaitForSeconds(2f);
            explosionpart.Play();
        explosao = true;
        FindObjectOfType<soundManager>().Play("barrelexplosion");


    }

   
}
