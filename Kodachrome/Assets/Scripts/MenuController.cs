using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    public GameObject menu;
    public GameObject video;

    public Text kick;
    public Text minds;
    public Text hazard;
    
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.me.gameState == Global.GameState.Menu)
        {
            menu.SetActive(true);
            video.GetComponent<Playback>().Pause();
            
            if (Input.GetKeyUp(KeyCode.Escape) && Global.me.pauseControls == false)
            {
                Global.me.gameState = Global.GameState.Game;
                video.GetComponent<Playback>().Play();
                Global.me.pauseControls = true;
            }

            if (Global.me.focalP1)
            {
                kick.color = Color.green;
            }
            if (Global.me.focalP2)
            {
                minds.color = Color.green;
            }
            if (Global.me.focalP3)
            {
                hazard.color = Color.green;
            }
        }
        else
        {
            menu.SetActive(false);
        }

        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKey(KeyCode.RightShift))
        {
            Application.Quit();
        }
        
        //impliment checklist update
        
    }
}
