using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Global : MonoBehaviour
{
    public static Global me;

    public bool pauseControls;
    private float pauseTime = .1f;
    
    public List<GameObject> photoList;
    

    public bool done = false;

    //checks for the game over stuff
    public bool focalP1;
    public bool focalP2;
    public bool focalP3;

    public double videoTime = 0;

    public bool canTake;
    
    public enum PlaybackState
    {
        Restart,
        Forward,
        Back,
        Play,
        Pause
    }
    public PlaybackState playbackState;
    

    public enum GameState
    {
        None,
        Game,
        Gallery,
        Menu,
        StartScreen
    }
    public GameState gameState;
    
    
    public enum CameraState
    {
        telephoto,
        midrange,
        wide
    }
    public CameraState cameraState;
    
    
    void Awake()
    {
        me = this;
    }

    void Update()
    {
        if (pauseControls)
        {
            pauseTime -= Time.deltaTime;
        }

        if (pauseTime < 0)
        {
            pauseControls = false;
            pauseTime = .1f;
        }
        
    }
}
