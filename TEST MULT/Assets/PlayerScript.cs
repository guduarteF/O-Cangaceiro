using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;
public class PlayerScript : NetworkBehaviour
{
    public float velocidade;
    private Rigidbody rb;
    private float movimentoX,movimentoZ;
    public GameObject bullet;

   
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if(isLocalPlayer)
        {
            movimentoX = Input.GetAxis("Horizontal");
            movimentoZ = Input.GetAxis("Vertical");

            rb.velocity = new Vector3(movimentoX * velocidade, 0, movimentoZ * velocidade);

            if (Input.GetKeyDown(KeyCode.E))
            {
                CmdSpawnBullet();
            }
        }
      
    }

    [Command]
    void CmdSpawnBullet()
    {
        GameObject cloneBullet = Instantiate(bullet, gameObject.transform.position, gameObject.transform.rotation);
        NetworkServer.Spawn(cloneBullet);
    }
}
