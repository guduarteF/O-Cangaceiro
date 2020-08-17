using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bala : MonoBehaviour
{
    public float speed = 2;
    public ParticleSystem hit_Effect;
    //[SerializeField] private float knockbackStrengh;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.right * speed;
        transform.Rotate(0f, 0f, -90f);
        
    }

    // Update is called once per frame
    void Update()
    {
      
        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Instantiate(hit_Effect, gameObject.transform.position, Quaternion.identity);
            hit_Effect.Play();
            //Rigidbody2D rb = collision.GetComponent<Collider2D>().GetComponent<Rigidbody2D>();
            //if (rb != null)
            //{
            //    Vector2 direction = collision.transform.position - transform.position;
            //    direction.y = 0;
            //    rb.AddForce(direction.normalized * knockbackStrengh, ForceMode2D.Impulse);
                
            //}
            

        }
    }

}
