using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasController : MonoBehaviour //game canvas controller
{
    public Text frameNum;

    public GameObject video;

    public Text wind;
    public Text lenses;
    
    public GameObject fast;
    public GameObject play;
    public GameObject back;
    
    public GameObject up;
    public GameObject down;
    public GameObject left;
    public GameObject right;
    public GameObject leftdown;
    public GameObject rightdown;
    public GameObject rightup;
    public GameObject leftup;
    
    public GameObject camera;
    public GameObject ui;

    public bool juice;
    
    public GameObject black;
    public float blackTime = .5f;
    public float blackTimer;
    
    public GameObject still;
    public float stillTime = 1f;
    public float stillTimer;

    // Start is called before the first frame update
    void Start()
    {
        stillTimer = stillTime;
        blackTimer = blackTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (Global.me.gameState == Global.GameState.Game)
        {
            if (Input.GetKeyUp(KeyCode.Escape)&& Global.me.pauseControls == false)
            {
                Global.me.gameState = Global.GameState.Menu;
                video.GetComponent<Playback>().Pause();
                Global.me.pauseControls = true;
            }
            if (Input.GetKeyUp(KeyCode.Tab) && Global.me.pauseControls == false)
            {
                Global.me.gameState = Global.GameState.Gallery;
                video.GetComponent<Playback>().Pause();
                Global.me.pauseControls = true;
            }
            if (Input.GetKeyUp(KeyCode.Space) && Global.me.canTake)
            {
                Debug.Log("check 1 2");
                ui.SetActive(false);
                juice = true;
            }
            else
            {
                Debug.Log("check 3 4");
                ui.SetActive(true);
                UpdateUI();
            }
        }
        else
        {
            ui.SetActive(false);
        }

        Juicing();
    }

    void Juicing()
    {
        if (juice)
        {
            Global.me.canTake = false;
            video.GetComponent<Playback>().Pause();
            if (blackTimer > 0)
            {
                blackTimer -= Time.deltaTime;
                black.SetActive(true);
            }
            else
            {
                still.GetComponent<RawImage>().texture = camera.GetComponent<Click>().lastPhoto; 
                black.SetActive(false);
                still.SetActive(true);
                stillTimer -= Time.deltaTime;
            }

            if (stillTimer < 0)
            {
                still.SetActive(false);
                black.SetActive(false);
                blackTimer = blackTime;
                stillTimer = stillTime;
                video.GetComponent<Playback>().Play();
                juice = false;
                
            }
        }
    }

    void UpdateUI()
    {
        if (Global.me.cameraState == Global.CameraState.wide)
        {
            lenses.text = "24mm";
        }
        else if (Global.me.cameraState == Global.CameraState.midrange)
        {
            lenses.text = "52mm";
        }
        else
        {
            lenses.text = "85mm";
        }

        if (Global.me.cameraState != Global.CameraState.wide)
        { 
            if (Input.GetKey(KeyCode.UpArrow))
            {
                up.SetActive(true);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.DownArrow))
            {
                up.SetActive(false);
                down.SetActive(true);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.LeftArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(true);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.RightArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(true);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(true);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.UpArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(true);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.RightArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(true);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else if (Input.GetKey(KeyCode.DownArrow) && Input.GetKey(KeyCode.LeftArrow))
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(true);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
            else
            {
                up.SetActive(false);
                down.SetActive(false);
                left.SetActive(false);
                right.SetActive(false);
                leftdown.SetActive(false);
                rightdown.SetActive(false);
                rightup.SetActive(false);
                leftup.SetActive(false);
            }
        }else
        {
            up.SetActive(false);
            down.SetActive(false);
            left.SetActive(false);
            right.SetActive(false);
            leftdown.SetActive(false);
            rightdown.SetActive(false);
            rightup.SetActive(false);
            leftup.SetActive(false);
        }
        
        if (camera.GetComponent<Click>().wound == false)
        {
            wind.text = "Advance the Film!" + "\n" + "Hold Enter";
        }
        else
        {
            wind.text = "";
        }
        
        
        frameNum.text = "" + camera.GetComponent<Click>().numOfPhotos;
        
        if (Global.me.playbackState == Global.PlaybackState.Play)
        {
            play.SetActive(true);
            back.SetActive(false);
            fast.SetActive(false);
        }
        else if (Global.me.playbackState == Global.PlaybackState.Forward)
        {
            play.SetActive(false);
            back.SetActive(false);
            fast.SetActive(true);
        }
        else if (Global.me.playbackState == Global.PlaybackState.Back)
        {
            play.SetActive(false);
            back.SetActive(true);
            fast.SetActive(false);
        }
        else if (Global.me.playbackState == Global.PlaybackState.Restart)
        {
            play.SetActive(false);
            back.SetActive(true);
            fast.SetActive(false);
        }
    }
}
