using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FramesPerSecond : MonoBehaviour
{
    Rect fpsRect;
    GUIStyle style;
    float fps;
    void Start()
    {
        fpsRect = new Rect(100, 100, 400, 100);
        style = new GUIStyle();
        style.fontSize = 30;
        StartCoroutine(RecalculateFPS());
    }

    // Update is called once per frame
   private IEnumerator RecalculateFPS()
    {
        while(true)
        {
            fps = 1 / Time.deltaTime;
            yield return new WaitForSeconds(1);
        }
    }

     void OnGUI()
    {
        
        GUI.Label(fpsRect, "FPS : " + string.Format("{0:0.0}", fps),style);

    }
}
