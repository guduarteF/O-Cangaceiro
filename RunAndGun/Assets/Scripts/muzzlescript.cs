using UnityEngine;

public class muzzlescript : MonoBehaviour
{
    public static muzzlescript ms;
    public Transform spawnPos;
    public ParticleSystem esplosao;
    public Transform spawnEnemy;
    void Start()
    {
        ms = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void animFlash()
    {
        transform.position = new Vector2(spawnPos.position.x, spawnPos.position.y);
        int count = 0;
        if(count == 0)
        {
            ParticleSystem cloneflash = Instantiate(esplosao, spawnPos.position, Quaternion.identity);
            count++;
        }
       
        GetComponent<Animator>().Play("muzzleflashanim", -1, 0f);
        
    }

    public void AnimFlashE()
    {
        GetComponent<Animator>().Play("muzzleflashanim", -1, 0f);
    }
}
