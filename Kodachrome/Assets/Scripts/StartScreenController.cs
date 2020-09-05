using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreenController : MonoBehaviour
{
    public AudioSource shutter;
    public GameObject screen;
    public Text instructions;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.me.focalP1 && Global.me.focalP2 && Global.me.focalP3)
        {
            Global.me.done = true;
        }
        
        if (Global.me.gameState == Global.GameState.StartScreen)
        {
            screen.SetActive(true);
            if (Input.GetKey(KeyCode.Escape))
            {
                Application.Quit();
            }

            if (Input.GetKey(KeyCode.R) && Input.GetKey(KeyCode.LeftShift))
            {
                Scene scene = SceneManager.GetActiveScene(); 
                SceneManager.LoadScene(scene.name);
            }
            
            
            
            if (Global.me.done)
            {
                
                instructions.text = "Thanks for playing! (Crtl + R to restart)";
            }
            else
            {
                instructions.text = "Press the Spacebar to Start";
                if (Input.GetKey(KeyCode.Space))
                {
                    shutter.PlayOneShot(shutter.clip);
                    Global.me.gameState = Global.GameState.Menu;
                    screen.SetActive(false);
                }
            }
        }

        if (Global.me.done)
        {
            Global.me.gameState = Global.GameState.StartScreen;
        }

        
    }
}
