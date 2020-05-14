using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class passaro_Script : MonoBehaviour
{
    public Rigidbody2D rb;
    public float velocidade_x;
    public float velocidade_y;
    public Transform pos;
    private bool preparado;
    public Transform player_pos;
    public float moveSpeed = 5f;
    private Vector2 movement;
    public float thrust;
    
   

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 direction = player_pos.position - transform.position;
        float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
        rb.rotation = angle;
        direction.Normalize();
        movement = direction;
        rb.velocity = new Vector2(-velocidade_x, velocidade_y);

        if(pos.position.y >= 180)
        {
            velocidade_x = 0;
            velocidade_y = 0;
            preparado = true;
        }

        if(preparado == true)
        {
           
            
            moveCharacter(movement);
        }
    }

    void moveCharacter(Vector2 direction)
    {
        rb.MovePosition((Vector2)transform.position + (direction * moveSpeed * Time.deltaTime));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
      
            
            Destroy(gameObject);


        

    }
}
