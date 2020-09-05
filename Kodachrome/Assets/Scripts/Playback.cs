using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.Video;

public class Playback : MonoBehaviour
{
    public float baseSpeed;
    private float skipSpeed;
    public float speedup = .5f;
    public float revert = 1f;
    
    private float pressTimer;
    public float PressTime = 1f;
    public bool pressed;

    public VideoPlayer video;
    
    void Start()
    {
        video = this.GetComponent<VideoPlayer>();
        baseSpeed = 1f;
        skipSpeed = (baseSpeed + speedup);
        pressTimer = PressTime;
        
    }
    
    void Update()
    {
        Global.me.videoTime = video.time;
        if (Global.me.gameState == Global.GameState.Game)
        {
            Cntrls(); 
        }
        else
        {
            video.playbackSpeed = 0f;
        }
        
    }

    public void Cntrls()
    {
        if (Input.GetKeyDown(KeyCode.Comma) && pressed == false)
        {
            Reverse(Time.deltaTime);
        }
        if(Input.GetKeyUp(KeyCode.Comma) && pressed == false)
        {
            Play();
            pressed = true;
        }
        if (pressed)
        {
            pressTimer -= Time.deltaTime;
        }

        if (pressTimer > 0 && pressed && Input.GetKeyDown(KeyCode.Comma))
        {
            Restart();
        }

        if (pressed && pressTimer < 0)
        {
            pressTimer = PressTime;
            pressed = false;
        }


        if (Input.GetKeyUp(KeyCode.Slash))
        {
            Play();
        }

        
        if (Input.GetKeyDown(KeyCode.Period))
        {
            Forward();
        }else if (Input.GetKeyUp(KeyCode.Period))
        {
            Play();
        }
    }


    public void Play()
    {
        video.playbackSpeed = baseSpeed;
        Global.me.playbackState = Global.PlaybackState.Play;
    }
    
    public void Restart()
    {
        Global.me.playbackState = Global.PlaybackState.Restart;
        video.time = 0;
        pressed = false;
        Play();
    }

    public void Forward()
    {
        Global.me.playbackState = Global.PlaybackState.Forward;
        video.playbackSpeed = skipSpeed;
    }
    
    public void Reverse(float deltaTime)
    {
        Global.me.playbackState = Global.PlaybackState.Back;
        video.playbackSpeed = 1;
        video.time -= revert;
    }

    public void Pause()
    {
        Global.me.playbackState = Global.PlaybackState.Pause;
        video.playbackSpeed = 0;
    }
}
