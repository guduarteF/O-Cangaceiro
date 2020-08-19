using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Criação das Variáveis
    [SerializeField] private LayerMask platformLayerMask;
    public float velocidade, impulso = 1.5f, thrust, comewithme, movimento;
    public bool isgrounded, correndo, troca = false, gameover_bool,ismoving;
    public GameObject bala, municaocheia0, municaocheia1, municaocheia2, municaocheia3, municaocheia4, chapeu_0, chapeu_1, chapeu_2, gameover, loadingpistol,loadingshotgun,loadingsniper;
    public Transform spawnPoint;
    public int balaspistol = 5, vida = 3, count = 0;
    private int balasshotgun = 3, balassniper = 6;
    private int numero;
    public ParticleSystem poeira;
    private CapsuleCollider2D boxCollider2d;
    public static Player player_script;
    public Rigidbody2D rb;
    private AudioSource audiosrc;
    public int number;
    public GameObject placa;
    public GameObject placa2;
    private float distancia;
    private float distancia2;
    public GameObject dialog;
    public GameObject dialog2;
    public GameObject Ebutton;
    public GameObject Ebutton2;
    public float pistol_fire_rate, sniper_fire_rate, shotgun_fire_rate;
    private float tempoentretirospistol;
    private float tempoentretirossniper;
    private float tempoentretirosshotgun;
    private bool colide_parede;
    public static Player plyr;
    private bool isreloading;
    public Joystick joystick;
    private int contador;
    private bool isSliding = false;
    public CapsuleCollider2D regularcoll;
    public CapsuleCollider2D slidingcoll;
    public float slideSpeed;
    public int countslide = 0;
    public Transform groundCheck;
    public float checkRadius;
    public LayerMask whatisGround;
    public bool isTouchingFront;
    public Transform frontCheck;
    bool wallSliding;
    public float wallSlidingSpeed;
    bool isGrounded;
    bool wallJumping;
    public float xWallForce;
    public float yWallForce;
    public float WallJumpTime;
    public bool shotgun, pistol, sniper;
    public float extraHeightText = 20f;
    public GameObject sniperBullet;
    public LayerMask enemylayer;
    public RaycastHit2D pontodecolisao;
    public GameObject pistolimage, sniperimage, shotgunimage;
    public GameObject blood;
    public GameObject pistolhud, sniperhud, shotgunhud;
    public GameObject municaocheia0sniper, municaocheia1sniper, municaocheia2sniper, municaocheia3sniper, municaocheia4sniper, municaocheia5sniper, municaocheia0shotgun, municaocheia1shotgun, municaocheia2shotgun;
    #endregion
    #region Start
    void Start() 
    {
        pistol = true;
        pistolimage.SetActive(true);
        sniperimage.SetActive(false);
        shotgunimage.SetActive(false);      
        audiosrc = GetComponent<AudioSource>();
        gameover_bool = false;
        player_script = this;   
        loadingpistol.SetActive(false);
        loadingshotgun.SetActive(false);
        loadingsniper.SetActive(false);
        count = 1;
        Time.timeScale = 1f; 
        boxCollider2d = transform.GetComponent<CapsuleCollider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        tempoentretirospistol = 0;
        colide_parede = false;
        plyr = this;
        pistolhud.SetActive(true);
        movimento = 0f;

    }
    #endregion
    void Update()
    {
        if(GetComponent<SpriteRenderer>().flipX == true)
        {
            Debug.DrawRay(new Vector2(spawnPoint.position.x, spawnPoint.position.y), new Vector2(spawnPoint.position.x - 9999f, spawnPoint.position.y));
        }
        else
        {
            Debug.DrawRay(new Vector2(spawnPoint.position.x, spawnPoint.position.y), new Vector2(spawnPoint.position.x + 9999f, spawnPoint.position.y));
        }
       
        if (sniper)
        {
            shotgunhud.SetActive(false);
            pistolhud.SetActive(false);
            sniperhud.SetActive(true);
            
        }
        else if(shotgun)
        {
            sniperhud.SetActive(false);
            pistolhud.SetActive(false);
            shotgunhud.SetActive(true);
            
        }
        else
        {
            sniperhud.SetActive(false);
            shotgunhud.SetActive(false);
            pistolhud.SetActive(true);
            
        }
       
       
        if(Input.GetKeyUp(KeyCode.P) && menuManager.menu_ref.is_Pause == false)
        {
            menuManager.menu_ref.Pause();
        }
        else if (Input.GetKeyUp(KeyCode.P) && menuManager.menu_ref.is_Pause == true)
        {
            menuManager.menu_ref.voltar();
        }
        #region Movimento
       

        if(joystick.Horizontal >= .2f)
        {
            movimento = 1;
        }
        else if(joystick.Horizontal <= -.2f)
        {
            movimento = -1;
        }
        else
        {
            movimento = 0;
        }
        
        rb.velocity = new Vector2(movimento * velocidade, rb.velocity.y);
        float verticalMove = joystick.Vertical;
       
        if (verticalMove <= -.5f)
        {
            performSlide();
        }
        if (rb.velocity.x != 0)
        {
            ismoving = true;
        }
        else
        {
            ismoving = false;
        }

        if (ismoving == true)
        {
            if (isGrounded && !audiosrc.isPlaying)
            {
                audiosrc.Play();
            }
            if (!isGrounded)
            {
                audiosrc.Stop();
            }
            if(menuManager.menu_ref.is_Pause == true)
            {
                audiosrc.Stop();
            }
        }
        else
        {
            audiosrc.Stop();
        }

        if (movimento < 0 && troca == false) 
        {
            spawnPoint.transform.Rotate(0, 0, 180);
            troca = true;
        }
        if (movimento > 0 && troca == true) 
        {
            spawnPoint.transform.Rotate(0, 0, 180);
            troca = false;
        }

        if (movimento < 0 ) 
        {                     
            spawnPoint.position = new Vector2(this.transform.position.x - 30, spawnPoint.transform.position.y); 

            GetComponent<SpriteRenderer>().flipX = true; 
            correndo = true;
          
            if(numero == 0)
            {
                numero = 1;
            }          
        }
      
        if(movimento == 0) 
        {        
            correndo = false;
            GetComponent<Animator>().SetBool("Correndo", false); 
        }

        if(movimento > 0)
        { 
            spawnPoint.position = new Vector2(this.transform.position.x + 30, spawnPoint.transform.position.y);
            correndo = true;
            
            if(numero == 1)
            {
                Flip(); 
                numero = 0;
            }
        }
        
        if (correndo == true) 
        {
            GetComponent<Animator>().SetBool("Correndo", true);
        }
        #endregion

        #region Pulo
       
            
            if (isGrounded == false)
            {
                contador = 0;
                GetComponent<Animator>().SetBool("Correndo", false);
                GetComponent<Animator>().SetBool("pulo_ar", true);
            }
            if (isGrounded == true)
            {
                GetComponent<Animator>().SetBool("pulo_ar", false);
            }
        
        
           if(movimento <0)
           {
             frontCheck.transform.position =new Vector2(gameObject.transform.position.x + -20f, gameObject.transform.position.y + 0.36f);
           }
           else
           {
            frontCheck.transform.position = new Vector2(gameObject.transform.position.x + 20f, gameObject.transform.position.y + 0.36f);
            }
       
        isTouchingFront = Physics2D.OverlapCircle(frontCheck.position, checkRadius, whatisGround);
       
        if (isTouchingFront == true && isGrounded == false && movimento != 0)
        {
            wallSliding = true;
        }
        else
        {
            wallSliding = false;
        }


        if (wallSliding)
        {
            rb.velocity = new Vector2(rb.velocity.x, Mathf.Clamp(rb.velocity.y, -wallSlidingSpeed, float.MaxValue));
        }

        isGrounded = Physics2D.OverlapCircle(groundCheck.position, checkRadius, whatisGround);

        if(Input.GetKeyDown(KeyCode.Space) && wallSliding)
        {
            wallJumping = true;
            Invoke("SetWallJumpingToFalse", WallJumpTime);
        }

        if(wallJumping)
        {
            rb.velocity = new Vector2(xWallForce * -movimento, yWallForce);
        }


        #endregion

        #region Tiro , Recarregar , Balas

      
       
       
       
        switch (balaspistol)
        {

            case 0:
                municaocheia4.SetActive(false); 
                break; 
            case 1:
                municaocheia3.SetActive(false);
                break;
            case 2:
                municaocheia2.SetActive(false);
                break;
            case 3:
                municaocheia1.SetActive(false);
                break;
            case 4:
                municaocheia0.SetActive(false);
                break;

        }

        switch (balasshotgun)
        {
            case 0:
                municaocheia0shotgun.SetActive(false);
                break;
            case 1:
                municaocheia1shotgun.SetActive(false);
                break;
            case 2:
                municaocheia2shotgun.SetActive(false);
                break;
           
        }

        switch (balassniper)
        {
            case 0:
                municaocheia0sniper.SetActive(false);
                break;
            case 1:
                municaocheia1sniper.SetActive(false);
                break;
            case 2:
                municaocheia2sniper.SetActive(false);
                break;
            case 3:
                municaocheia3sniper.SetActive(false);
                break;
            case 4:
                municaocheia4sniper.SetActive(false);
                break;
            case 5:
                municaocheia5sniper.SetActive(false);
                break;

        }

        #endregion


        #region Vidas
     
       

        if (vida <= 0) 
        {
            chapeu_0.SetActive(false);
            chapeu_1.SetActive(false);
            chapeu_2.SetActive(false);
            GetComponent<Animator>().SetBool("Morte", true);
            StartCoroutine(Morte());

        }
        else if (vida == 1)
        {
            chapeu_1.SetActive(false);
            chapeu_2.SetActive(false);
        }
        else if (vida == 2)
        {
            chapeu_2.SetActive(false);
        }
        #endregion

        #region Placas
        distancia = Vector2.Distance(gameObject.transform.position, placa.transform.position);
        distancia2 = Vector2.Distance(gameObject.transform.position, placa2.transform.position);

        if(distancia < 100)
        {
            dialog.GetComponent<Animator>().SetBool("placa3", false);
            Ebutton.GetComponent<Animator>().SetBool("placa2", true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                FindObjectOfType<soundManager>().Play("select");
                dialog.GetComponent<Animator>().SetBool("placa", true);
            }
        }
        if (distancia > 100)
        {
            Ebutton.GetComponent<Animator>().SetBool("placa2", false);
            dialog.GetComponent<Animator>().SetBool("placa3", true);
        }
        //----------------------------------
       if (distancia2 < 100)
        {
            dialog2.GetComponent<Animator>().SetBool("placa3", false);
            Ebutton2.GetComponent<Animator>().SetBool("placa2", true);
            if (Input.GetKeyUp(KeyCode.E))
            {
                FindObjectOfType<soundManager>().Play("select");
                dialog2.GetComponent<Animator>().SetBool("placa", true);
            }
        }
        if (distancia2 > 100)
        {
            Ebutton2.GetComponent<Animator>().SetBool("placa2", false);
            dialog2.GetComponent<Animator>().SetBool("placa3", true);
        }

        #endregion
    }
    #region On Colision e On trigger
    //On collision é para colisões em que os objetos interagem entre si. Sendo o Enter para a entrada de colisão e Exit a saída
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Chao"))
        {
            dust();
        }

       
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
       
    }
    // o trigger é para casos em que a colisão não precisa interagir.
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("limitemapa"))
        {
            vida = vida - 3;
            
        }
        rb = GetComponent<Rigidbody2D>();

        if (collision.gameObject.CompareTag("passaro"))
        {
           
             vida--;
            rb.AddForce(Vector2.up * 2000f , ForceMode2D.Force);
            GetComponent<Animator>().SetBool("hit", true);
            screenshake.instance.StartShake(.2f, .1f);
            StartCoroutine(sairHit());

            
        }
       
        
           
        

        
    }
    private void OnTriggerExit2D(Collider2D collision)
    {


        if (collision.gameObject.CompareTag("bala"))
        {
            FindObjectOfType<soundManager>().Play("player_hit");
            GetComponent<Animator>().SetBool("hit", true);            
            screenshake.instance.StartShake(.2f, .1f);
            vida--;
            StartCoroutine(sairHit());

        }
    }
    #endregion

    #region IEnumerator
   
    IEnumerator Atirar()
    {      
        muzzlescript.ms.animFlash(); 
       
        if(pistol)
        {
            balaspistol--;
            tempoentretirospistol = Time.time + pistol_fire_rate;
            FindObjectOfType<soundManager>().Play("player_shooting");
            GameObject cloneBullet = Instantiate(bala, spawnPoint.position, spawnPoint.rotation);
            yield return new WaitForSeconds(.7f);
            Destroy(cloneBullet); 
        }
        else if(sniper)
        {
            balassniper--;
            tempoentretirossniper = Time.time + sniper_fire_rate;
            FindObjectOfType<soundManager>().Play("sniper");
            //  RaycastHit2D hit = Physics2D.Raycast(spawnPoint.position, new Vector2(spawnPoint.position.x + 10f, spawnPoint.position.y), 999f, enemylayer);
            float f_direction;
          if(GetComponent<SpriteRenderer>().flipX == true)
            {
                f_direction = -9999f;
            }
          else
            {
                f_direction = 9999f;
            }
            RaycastHit2D[] hits = Physics2D.RaycastAll(new Vector2(spawnPoint.position.x, spawnPoint.position.y), new Vector2(spawnPoint.position.x + f_direction, spawnPoint.position.y), 999f, enemylayer);
           
            for (int i = 0; i < hits.Length; i++)
            {
                RaycastHit2D hit = hits[i];
                GameObject enemy = hit.collider.gameObject;
               
                if(enemy)
                {
                    Instantiate(blood, hit.transform.position, Quaternion.identity);
                    Destroy(enemy.gameObject);
                }
            }
            //if (hit)
            //{
            //    Instantiate(blood, hit.transform.position, Quaternion.identity);               
            //    Destroy(hit.collider.gameObject);
            //}
            Instantiate(sniperBullet, spawnPoint.position, spawnPoint.rotation);
            FindObjectOfType<soundManager>().Play("player_shooting");
        }
        else
        {
            FindObjectOfType<soundManager>().Play("shotgun");
            balasshotgun--;
            tempoentretirosshotgun = Time.time + shotgun_fire_rate;
            for (int i = 0; i < 10; i++)
            {
                
                if (GetComponent<SpriteRenderer>().flipX == true)
                {
                    float anglea = Random.Range(175, 185);
                    GameObject CloneBullet = Instantiate(bala, spawnPoint.transform.position, Quaternion.AngleAxis(anglea, Vector3.forward));
                    Destroy(CloneBullet, 1f);
                }
                else
                {
                    float anglea = Random.Range(-5, 5);
                    GameObject CloneBullet = Instantiate(bala, spawnPoint.transform.position, Quaternion.AngleAxis(anglea, -Vector3.forward));
                    Destroy(CloneBullet, 1f);
                }

            }
        }

        
      
       
    }
    
    IEnumerator Reload()
    {
        if(pistol)
        {
            loadingpistol.SetActive(true);
            isreloading = true;
            yield return new WaitForSeconds(2f);
            FindObjectOfType<soundManager>().Play("reload");
            balaspistol = 5;
            municaocheia0.SetActive(true);
            municaocheia1.SetActive(true);
            municaocheia2.SetActive(true);
            municaocheia3.SetActive(true);
            municaocheia4.SetActive(true);
            loadingpistol.SetActive(false);
            isreloading = false;
        }
        else if(shotgun)
        {
            loadingshotgun.SetActive(true);
            isreloading = true;
            yield return new WaitForSeconds(2f);
            FindObjectOfType<soundManager>().Play("reload");
            balasshotgun = 3;
            municaocheia0shotgun.SetActive(true);
            municaocheia1shotgun.SetActive(true);
            municaocheia2shotgun.SetActive(true);   
            loadingshotgun.SetActive(false);
            isreloading = false;
        }
        else
        {
            loadingsniper.SetActive(true);
            isreloading = true;
            yield return new WaitForSeconds(2f);
            FindObjectOfType<soundManager>().Play("reload");
            balassniper = 6;
            municaocheia0sniper.SetActive(true);
            municaocheia1sniper.SetActive(true);
            municaocheia2sniper.SetActive(true);
            municaocheia3sniper.SetActive(true);
            municaocheia4sniper.SetActive(true);
            municaocheia5sniper.SetActive(true);
            loadingsniper.SetActive(false);
            isreloading = false;
        }
       
    }
    IEnumerator Morte()
    {
        if (number == 0)
            FindObjectOfType<soundManager>().Play("limite_mapa_death");
        number++;
        screenshake.instance.StartShake(2, 1f);
        yield return new WaitForSeconds(1f);
        GetComponent<SpriteRenderer>().enabled = (false);
        FindObjectOfType<soundManager>().Play("player_death");
        if (menuManager.menu_ref.pause_bool != true)
        {
            gameover.SetActive(true);
            gameover_bool = true;
        }
        
        
        count = 0;
      
        if (count == 0)
        {
            Time.timeScale = 0f; 
        }
    }
    IEnumerator sairHit()
    {
        yield return new WaitForSeconds(.5f);
        GetComponent<Animator>().SetBool("hit", false);

    }
    #endregion
   
    #region Funções

    void SetWallJumpingToFalse()
    {
        wallJumping = false;
    }
    void dust()
    {
        poeira.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 30f); ;
        poeira.Play(); 
    }
    
    private void Flip() 
    {
        GetComponent<SpriteRenderer>().flipX = false; 
    }

    private void performSlide()
    {
      
        if(countslide == 0 )
        {
            isSliding = true;
            regularcoll.enabled = false;
            slidingcoll.enabled = true;
            if (movimento != 0)
            {
                velocidade = 300;
                
            }
            StartCoroutine(endSlide());
            countslide = 1;
        }
           
        
       


    }

    IEnumerator endSlide()
    {
        yield return new WaitForSeconds(0.8f);
        regularcoll.enabled = true;
        slidingcoll.enabled = false;
        isSliding = false;
        velocidade = 200;
        yield return new WaitForSeconds(1f);
        countslide = 0;
    }

   
    public void ButtonRecarregar()
    {
        if(pistol)
        {
            if (balaspistol < 5 && isreloading == false)
            {
                StartCoroutine(Reload());
            }
        }
        else if(sniper)
        {
            if (balassniper < 6 && isreloading == false)
            {
                StartCoroutine(Reload());
            }
        }
        else
        {
            if (balasshotgun < 3 && isreloading == false)
            {
                StartCoroutine(Reload());
            }
        }
        
    }

    public void Jump()
    {
        if (isGrounded && contador == 0) // Se apertar espaço e a função isGrounded retornar verdadeira então :
        {
            contador = 1;
            rb.AddForce(new Vector2(rb.velocity.x, impulso)); //AddForce adiciona uma força no eixo em questão(y)                                                            // GetComponent<Animator>().SetBool("Pulo", true); 
            dust();
            FindObjectOfType<soundManager>().Play("jump_player");

        }
    }

    public void shotgunweapon()
    {
        shotgun = true;
        pistol = false;
        sniper = false;
        pistolimage.SetActive(false);
        sniperimage.SetActive(false);
        shotgunimage.SetActive(true);
      
             
        
    }

    public void pistolweapon()
    {
        pistol = true;
        sniper = false;
        shotgun = false;
        pistolimage.SetActive(true);
        sniperimage.SetActive(false);
        shotgunimage.SetActive(false);
       
    }

    public void sniperweapon()
    {
        sniper = true;
        shotgun = false;
        pistol = false;
        pistolimage.SetActive(false);
        sniperimage.SetActive(true);
        shotgunimage.SetActive(false);

       
    }

    public void Tiro()
    {
        if(pistol)
        {
            if ((balaspistol > 0 && Time.time > tempoentretirospistol && isreloading == false)) //Se apertar control e as balas forem maior que zero então:
            {
                StartCoroutine(Atirar()); // Executa uma função Coroutine chamada Tempo
            }
            else if (balaspistol == 0)
            {
                FindObjectOfType<soundManager>().Play("no_ammo");
            }
        }
        else if(sniper)
        {
            if ((balassniper > 0 && Time.time > tempoentretirossniper && isreloading == false)) //Se apertar control e as balas forem maior que zero então:
            {
                StartCoroutine(Atirar()); // Executa uma função Coroutine chamada Tempo
            }
            else if (balassniper==0)
            {
                FindObjectOfType<soundManager>().Play("no_ammo");
            }
        }
        else
        {
            if ((balasshotgun > 0 && Time.time > tempoentretirosshotgun && isreloading == false)) //Se apertar control e as balas forem maior que zero então:
            {
                StartCoroutine(Atirar()); // Executa uma função Coroutine chamada Tempo
            }
            else if (balasshotgun == 0)
            {
                FindObjectOfType<soundManager>().Play("no_ammo");
            }
        }
       
    }



    


    #endregion
  






}
