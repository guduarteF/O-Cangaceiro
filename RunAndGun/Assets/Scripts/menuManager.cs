using UnityEngine;
using UnityEngine.SceneManagement;

public class menuManager : MonoBehaviour
{
    public int contador =0;
    public GameObject fundo;
    public bool pause_bool = false;
    public static menuManager menu_ref;
    public bool is_Pause;
    // Start is called before the first frame update
    void Start()
    {
        menu_ref = this;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Play()
    {
       
        SceneManager.LoadScene("SampleScene");
        FindObjectOfType<soundManager>().Play("tutorial");
    }
    public void Restart()
    {
        FindObjectOfType<soundManager>().Play("menu_click");
        SceneManager.LoadSceneAsync("SampleScene");
       
    }

    public void Menu()
    {
        FindObjectOfType<soundManager>().Play("menu_click");
        SceneManager.LoadSceneAsync("menu");
    }

    public void Quit()
    {
        Application.Quit();
        FindObjectOfType<soundManager>().Play("quit");
        
    }

    public void Pause()
    {
        is_Pause = true;
        if(Player.player_script.gameover_bool != true)
        {
            fundo.SetActive(true);
            FindObjectOfType<soundManager>().Play("menu_click");
            pause_bool = true;
        }
        
       Time.timeScale = 0f;
       
        
    }

    public void voltar()
    {
        is_Pause = false;
        FindObjectOfType<soundManager>().Play("menu_click");
        Time.timeScale = 1f;
        fundo.SetActive(false);
        pause_bool = false;
    }

    public void cantclick()
    {
        FindObjectOfType<soundManager>().Play("cant_click");
    }

    public void controles()
    {
        FindObjectOfType<soundManager>().Play("menu_click");
        SceneManager.LoadSceneAsync("controles");
    }

    public void voltarparamenu()
    {
        FindObjectOfType<soundManager>().Play("menu_click");
        SceneManager.LoadSceneAsync("menu");
    }
}
