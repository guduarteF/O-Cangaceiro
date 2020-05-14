using System.Collections;
using UnityEngine;

public class Player : MonoBehaviour
{
    #region Criação das Variáveis
    [SerializeField] private LayerMask platformLayerMask;
    public float velocidade, impulso = 1.5f, thrust, comewithme, movimento;
    public bool isgrounded, correndo, troca = false, gameover_bool,ismoving;
    public GameObject bala, municaocheia0, municaocheia1, municaocheia2, municaocheia3, municaocheia4, chapeu_0, chapeu_1, chapeu_2, gameover, loading;
    public Transform spawnPoint;
    public int balas = 5, vida = 3, count = 0;
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
    public float fire_rate;
    private float tempoentretiros;
    private bool colide_parede;
    public static Player plyr;
    private bool isreloading;

    public float extraHeightText = 20f;
    #endregion
    #region Start
    void Start() //O Start executa sempre que o jogo for iniciado , apenas UMA vez.
    {
        //Toda atribuição de variáveis devem ser feitas no Start(). No update elas irão ser chamadas a cada frame , e isso afeta no desempenho do jogo
        // Essa atribuições são feitas para acelerar a produtividade do código. Assim eu posso chamar uma função grande com apenas poucos dígitos.
        audiosrc = GetComponent<AudioSource>();
        gameover_bool = false;
        player_script = this;   
        loading.SetActive(false);
        count = 1;
        Time.timeScale = 1f; // O Time.timeScale manipula a velocidade do jogo , faz câmera lenta ou acelera o jogo. O valor varia entre 0f - 2f . Sendo 0 (pause) , 0.5(câmera lenta), 1 (velocidade normal) , 2 (dobro da velocidade).
        boxCollider2d = transform.GetComponent<CapsuleCollider2D>();
        Rigidbody2D rb = GetComponent<Rigidbody2D>();
        tempoentretiros = 0;
        colide_parede = false;
        plyr = this;

    }
    #endregion
    void Update()
    {      

        if(Input.GetKeyUp(KeyCode.P) && menuManager.menu_ref.is_Pause == false)
        {
            menuManager.menu_ref.Pause();
        }
        else if (Input.GetKeyUp(KeyCode.P) && menuManager.menu_ref.is_Pause == true)
        {
            menuManager.menu_ref.voltar();
        }
        #region Movimento
        // O movimento recebe o Input.GetAxis . Um valor que varia entre -1 e 1 . E o Horizontal recebe do teclado o A (esquerda -1) D(direita 1) e as setas  <- -> . O player parado = 0.
        if (colide_parede == true)
        {
            movimento = 0;
        }
        movimento = Input.GetAxis("Horizontal"); 
        rb.velocity = new Vector2(movimento * velocidade, rb.velocity.y);

       
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
            if (isGrounded() && !audiosrc.isPlaying)
            {
                audiosrc.Play();
            }
            if (!isGrounded())
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

        if (movimento < 0 && troca == false) //Se o movimento for menor que 0 e o booleano troca for falso o spawnPoint(gameobject definido para a saído do tiro) irá rotacionar 180 graus.
        {
            spawnPoint.transform.Rotate(0, 0, 180);
            troca = true;


        }
        if (movimento > 0 && troca == true) // Se o movimento for maior que 0 e o booleano for verdadeiro ele irá rotacionar novamente
        {
            spawnPoint.transform.Rotate(0, 0, 180);
            troca = false;
        }

        if (movimento < 0 ) 
        {
           
           
            spawnPoint.position = new Vector2(this.transform.position.x - 30, spawnPoint.transform.position.y); // Se o movimento for menor que zero o spawnpoint muda de posição horizontal (x) , o y e o z se mantem. Ele também chama a função Flip() que flipa o Sprite do player.

            GetComponent<SpriteRenderer>().flipX = true; //flipar o objeto horizontalmente. flipY fliparia verticalmente.
            correndo = true;
          
            if(numero == 0)
            {
                numero = 1;
            }
            
        }
      
        if(movimento == 0) 
        {        
            correndo = false;
            GetComponent<Animator>().SetBool("Correndo", false); //Pega o component animator e torna o booleano falso. Desativando a animação de corrida
        }

        if(movimento > 0)
        { 
            spawnPoint.position = new Vector2(this.transform.position.x + 30, spawnPoint.transform.position.y);
            correndo = true;
            
            if(numero == 1)
            {
                Flip(); //Uma função definida fora do update que eu dei o nome de Flip.
                numero = 0;
            }
        }
        
        if (correndo == true) 
        {
            GetComponent<Animator>().SetBool("Correndo", true);
        }
        #endregion

        #region Pulo
        if (Input.GetKeyDown("space") && isGrounded()) // Se apertar espaço e a função isGrounded retornar verdadeira então :
        {
            rb.AddForce(new Vector2(rb.velocity.x, impulso)); //AddForce adiciona uma força no eixo em questão(y)
           // GetComponent<Animator>().SetBool("Pulo", true); 
            dust();
            FindObjectOfType<soundManager>().Play("jump_player");
           
        }
        if (isGrounded() == false)
        {
            GetComponent<Animator>().SetBool("Correndo", false);
            GetComponent<Animator>().SetBool("pulo_ar", true);
        }
        if (isGrounded() == true)
        {
            GetComponent<Animator>().SetBool("pulo_ar",false);
        }




        #endregion

        #region Tiro , Recarregar , Balas

        if ((Input.GetKeyUp(KeyCode.K) && balas>0 && Time.time > tempoentretiros && isreloading == false)) //Se apertar control e as balas forem maior que zero então:
        {
            FindObjectOfType<soundManager>().Play("player_shooting");
            muzzlescript.ms.animFlash(); //referencia a um script para executar o particleSystem (efeito visual do clarão do tiro)
            balas--; // diminui uma bala do pente
           
            StartCoroutine(Tempo()); // Executa uma função Coroutine chamada Tempo
        }
        if(Input.GetKeyUp(KeyCode.LeftControl) && balas ==0)
        {
            FindObjectOfType<soundManager>().Play("no_ammo");
        }
       
        if (Input.GetKeyUp(KeyCode.R) && balas<5 && isreloading == false)
        {
            StartCoroutine(Reload()); 
        }
        switch (balas) //Switch é um condicional parecido com o if , ele verifica cada valor para uma variável (balas nesse caso) e se ele for = a um desses casos executa um comando:
        {

            case 0:
                municaocheia4.SetActive(false); //Desativa um gameobject
                break; //O break encerra o loop , para não chamar a mesma função inumeras vezes.
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
        if (collision.gameObject.CompareTag("Chao")) //se o gameobject (o objeto que o script está inserido) colidir com um objeto com Tag 'Chão' então:
        {
            dust(); // chama a função dust();
        }

        if(collision.gameObject.CompareTag("parede"))
        {
            colide_parede = true;
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
            screenshake.instance.StartShake(.2f, .1f); //screenshake é o nome de um script , instance o nome de referência dele. StartShake é uma função do Unity quer recebe duas variáveis (tamanho e força , o quanto ele pode mexer a tela e a força disso)
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
    //Essa função Tempo que eu criei irá instaciar uma bala clone (clonebullet) do prefab chamado bala e esperar 2 segundos até ser destruido.
    IEnumerator Tempo() // IEnumerator é uma função em que você pode manipular o tempo em que será executado algo . Pra que isso ocorra 3 coisas devem ser feitas : 1-Criar uma função IEnumerator 2-Dentro dela escolher o tempo que vai esperar yield return new WaitForSeconds(2f); [2 segundos nesse caso] e 3-No lugar do código que você quiser chamar essa função StartCoroutine(nomedafunção());
    {
        tempoentretiros = Time.time + fire_rate;
        GameObject cloneBullet = Instantiate(bala, spawnPoint.position, spawnPoint.rotation); // Cria um GameObject chamado cloneBullet que é valido apenas nessa função (Toda variável criada dentro de uma função é valida apenas nela) . E Instancia esse gameobject. O Instantiate recebe 3 valores(gameobject original que será clonado,posicao , rotação)
        yield return new WaitForSeconds(.7f);
        Destroy(cloneBullet); //Destroy irá destruir algo que estará entre parenteses , deletar da cena.
    }
    
    IEnumerator Reload()
    {
        loading.SetActive(true);
        isreloading = true;
        yield return new WaitForSeconds(2f);
        FindObjectOfType<soundManager>().Play("reload");
        balas = 5;
        municaocheia0.SetActive(true);
        municaocheia1.SetActive(true);
        municaocheia2.SetActive(true);
        municaocheia3.SetActive(true);
        municaocheia4.SetActive(true);
        loading.SetActive(false);
        isreloading = false;
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
    void dust()
    {
        poeira.transform.position = new Vector2(gameObject.transform.position.x, gameObject.transform.position.y - 30f); ;
        poeira.Play(); //Poeira é uma variavel que eu criei , quando a função dust() for chamada irá dar play na particula.
    }
    private bool isGrounded()
    {
        RaycastHit2D raycastHit = Physics2D.Raycast(boxCollider2d.bounds.center, Vector2.down, boxCollider2d.bounds.extents.y + extraHeightText, platformLayerMask); //Raycast , é um raio que é emitido a uma direção para executar algum codigo. Nesse caso é criado uma variável chamada raycastHit que é do tipo RaycastHit2d e ela irá receber um RayCast . O a função Raycast parte do Physics2D e recebe 3 valores:( Vector2 origin , vector2 direção, float distance , int layermask) O layer mask é a camada que será afetada , nesse caso o platformLayer mask é uma variavel que eu criei e dentro do jogo eu arrastei o tilemap do chão para ela. Ou seja o raycast só irá afetar o chão.
        Color rayColor; //Criei uma variavel de cor chamado rayColor
        if (raycastHit.collider != null) // se a colisão desse raycastHit for diferente(!=) de nula então:
        {
            
            rayColor = Color.green; //cor do raio recebe verde
        }
        else // senão
        {
           
            rayColor = Color.red; // cor do raio recebe vermelho
        }
          Debug.DrawRay(boxCollider2d.bounds.center, Vector2.down*(boxCollider2d.bounds.extents.y + extraHeightText),rayColor); // Debug serve para jogar no console um valor de uma variavel , função ou texto. Nesse caso o DrawRay iria desenhar uma linha dentro da cena do jogo .
        return raycastHit.collider != null; // retorna para a função isGrounded que é booleana a colisão do raycast diferente de nula. Ou seja quando eu chama a função isGrounded ela será verdadeira caso a colisão seja verdadeira
    }
    private void Flip() 
    {
        GetComponent<SpriteRenderer>().flipX = false; 
    }

    #endregion







}
