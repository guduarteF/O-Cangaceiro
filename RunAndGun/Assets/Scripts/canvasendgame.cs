using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class canvasendgame : MonoBehaviour
{
    public GameObject fundo;
    public static canvasendgame c;
    public bool game_ended;
    void Start()
    {
        c = this;
    }

    // Update is called once per frame
    void Update()
    {
        if (GameObject.FindGameObjectsWithTag("Enemy").Length == 0 && Player.plyr.vida>0)
        {
           
            fundo.SetActive(true);
            Time.timeScale = 0f;
            game_ended = true;


        }
    }

   
}
