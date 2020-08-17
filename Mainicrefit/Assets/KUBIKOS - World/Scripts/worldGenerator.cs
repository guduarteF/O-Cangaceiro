using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class worldGenerator : MonoBehaviour
{
   private GameObject[,,] world;
    public GameObject grass;
    public GameObject dirt;
    void Start()
    {
        world = new GameObject[50, 50, 50];
        worldGen();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void worldGen()
    {

       for(int xx = 0; xx<world.GetLength(0);xx++)
       {
           
            for(int yy=0; yy<world.GetLength(1);yy++)
            {
                if (Random.Range(0, 100) < 50 && yy!=0 && xx!=0 )
                {
                    xx -= 1;
                    
                    
                }
                for (int zz = 0; zz < world.GetLength(2); zz++)
                {
                   
                        if (yy == world.GetLength(1) - 1)
                        {
                            world[xx, yy, zz] = Instantiate(grass, new Vector3(xx, yy, zz), Quaternion.identity);
                        }
                        else
                        {
                            world[xx, yy, zz] = Instantiate(dirt, new Vector3(xx, yy, zz), Quaternion.identity);
                        }
                    
                   
                    
                }
            }
        }
    }

}
