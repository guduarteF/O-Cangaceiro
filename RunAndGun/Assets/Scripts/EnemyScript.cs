using System.Collections;
using UnityEngine;

public class EnemyScript : MonoBehaviour
{
    
    public float velocidade;
    public GameObject bala;
    public Transform spawn_point;
    public float fire_rate;
    public float next_fire;
    private float distance;
    public GameObject player;
    public ParticleSystem blood;
    public Transform flashref;
    public ParticleSystem flashpart;
    public int distmin;
    public bool morreu;
    public static EnemyScript enemyscripref;
    private RaycastHit2D ponto;
    public LayerMask playerlayer;
    private float maxdistance = 250f;
    private CapsuleCollider2D boxcol;
    private float movimento;
    private bool troca;
    private bool troca2;
    public bool enemycamper;
    
    

    void Start()
    {
        boxcol = transform.GetComponent<CapsuleCollider2D>();
        enemyscripref = this;
        troca = false;
        troca2 = false;
        
    }

    // Update is called once per frame
    void Update()
    {
        distance = Vector2.Distance(player.transform.position, gameObject.transform.position);


        if (distance < distmin && enemycamper == false)
        {
            Rigidbody2D rb = GetComponent<Rigidbody2D>();
            rb.velocity = new Vector2(velocidade, rb.velocity.y);
        }


        if (Time.time > next_fire && distance < distmin)
        {
            StartCoroutine(Atirar());
        }

        //if(tnt.tntscript.explosao ==true)
        //{
        //    sangue();
        //    StartCoroutine(Death());
        //}

        if (Physics2D.Raycast(boxcol.bounds.center, Vector2.right, boxcol.bounds.extents.x + maxdistance, playerlayer))
        {
            velocidade = 100;
            GetComponent<SpriteRenderer>().flipX = false;
            troca2 = true;
            
        }
        Debug.DrawRay(boxcol.bounds.center, Vector2.right * (boxcol.bounds.extents.x + maxdistance), Color.red);
        if (GetComponent<SpriteRenderer>().flipX == false && troca == false)
        {
            spawn_point.transform.Rotate(0, 0, 180);
            spawn_point.position = new Vector2(this.transform.position.x + 30, spawn_point.transform.position.y);
            troca = true;
            
        }
             
        if (Physics2D.Raycast(boxcol.bounds.center, Vector2.left, boxcol.bounds.extents.x + maxdistance, playerlayer))
        {
            velocidade = -100;
            GetComponent<SpriteRenderer>().flipX = true;
            
        }
        Debug.DrawRay(boxcol.bounds.center, Vector2.left * (boxcol.bounds.extents.x + maxdistance), Color.green);
        if (GetComponent<SpriteRenderer>().flipX == true && troca == true)
        {
            spawn_point.transform.Rotate(0, 180, 0);
            spawn_point.position = new Vector2(this.transform.position.x - 30, spawn_point.transform.position.y);
            troca = false;
            

        }
    }

    IEnumerator Atirar()
    {
        
        next_fire = Time.time + fire_rate;
            yield return new WaitForSeconds(.5f);
        FindObjectOfType<soundManager>().Play("enemy_shooting");
        AnimExplosion();
        GameObject clone_bullet = Instantiate(bala, spawn_point.position, spawn_point.rotation);
        yield return new WaitForSeconds(2f);
            Destroy(clone_bullet);
            
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
       
        if(collision.gameObject.CompareTag("balaPlayer"))
        {
            
            StartCoroutine(Death());
            sangue();
            

        }

        if (collision.gameObject.CompareTag("limitemapa"))
        {
            StartCoroutine(Death());
            sangue();

        }


    }

    IEnumerator Death()
    {
        FindObjectOfType<soundManager>().Play("enemy_hit");
        morreu = true;
        Destroy(gameObject);
        yield return new WaitForSeconds(1f);
    }

    public void sangue()
    {
        Instantiate(blood,transform.position,Quaternion.identity);
        
    }

    public void AnimExplosion()
    {
        flashref.transform.position = new Vector2(spawn_point.position.x, spawn_point.position.y);
        Instantiate(flashpart, spawn_point.position, Quaternion.identity);
        muzzlescript.ms.AnimFlashE();
    }

    
}
