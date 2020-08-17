using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class gametimer : MonoBehaviour
{
    public Text timerText;
    private float starttime;
    public Text recordText;
    public Text newrecordText;
    private float f_seconds;
    private int contador;
    public static gametimer g;
    // Start is called before the first frame update
    void Start()
    {
        starttime = Time.time;
        recordText.text = PlayerPrefs.GetFloat("HighScore",0).ToString("f2");
        g = this;
        
    }

    // Update is called once per frame
    void Update()
    {

        if(Input.GetKeyDown(KeyCode.M))
        {
            ResetHighScore();
            
        }
        float t = Time.time - starttime;
        f_seconds = t;
        string seconds = t.ToString("f2");
        timerText.text = seconds;

        if (canvasendgame.c.game_ended == true)
        {
            HighScore();
        }

        void HighScore()
        {
            
           if(PlayerPrefs.GetFloat("HighScore",0) == 0)
            {
                PlayerPrefs.SetFloat("HighScore", f_seconds);
                recordText.text = f_seconds.ToString("f2");
            }
           else if(f_seconds < PlayerPrefs.GetFloat("HighScore", 0))
            {
                PlayerPrefs.SetFloat("HighScore", f_seconds);
                recordText.text = f_seconds.ToString("f2");
                newrecordText.gameObject.SetActive(true);


            }


         
           

        }

       

       
    }

    void ResetHighScore()
    {
        PlayerPrefs.DeleteAll();
        recordText.text = "0";
    }

  
}
