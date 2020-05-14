using UnityEngine;

public class balaPlayer : MonoBehaviour
{
    public float speed = 2;
    public ParticleSystem hit_Effect;

    void Start()
    {
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        rb.velocity = transform.up * speed;
        transform.Rotate(0f, 0f, 0f);

    }

    // Update is called once per frame
    void Update()
    {


    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        screenshake.instance.StartShake(.5f, .5f);
        FindObjectOfType<soundManager>().Play("hit");
        Destroy(gameObject);
        if (collision.gameObject.CompareTag("Enemy"))
        {
            Instantiate(hit_Effect, gameObject.transform.position, Quaternion.identity);
            hit_Effect.Play();
            screenshake.instance.StartShake(1f, 2f);
            Destroy(gameObject);
            

        }
    }

    
}
