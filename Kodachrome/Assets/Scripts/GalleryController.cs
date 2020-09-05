using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GalleryController : MonoBehaviour
{
    public int galleryNum = 0;

    public GameObject video;

    public GameObject gallery;

    // Update is called once per frame
    void Update()
    {
        if (Global.me.gameState == Global.GameState.Gallery && Global.me.photoList != null)
        {
            gallery.SetActive(true);
            Cntrls();
            Display();
        }
        else if (Global.me.gameState == Global.GameState.Gallery && Global.me.photoList == null)
        {
            gallery.SetActive(false);
            Global.me.gameState = Global.GameState.Game;
            video.GetComponent<Playback>().Play();
            galleryNum = 0;
        }
    }

    void Cntrls()
    {
        if (Input.GetKeyUp(KeyCode.LeftArrow)) 
        {
            if (galleryNum > 0)
            {
                galleryNum -= 1; 
            }
        } 
        else if (Input.GetKeyUp(KeyCode.RightArrow)) 
        {
            if (galleryNum < Global.me.photoList.Count)
            {
                galleryNum += 1; 
            }
        } 
        else if (Input.GetKeyUp(KeyCode.Tab) && Global.me.pauseControls == false) 
        {
            Global.me.gameState = Global.GameState.Game;
            video.GetComponent<Playback>().Play();
            galleryNum = 0;
            gallery.SetActive(false);
            Global.me.pauseControls = true;
        } 
        else if (Input.GetKeyUp(KeyCode.Escape) && Global.me.pauseControls == false) 
        {
            Global.me.gameState = Global.GameState.Menu;
            gallery.SetActive(false);
            Global.me.pauseControls = true;
        }
        
    }

    void Display()
    {
        for (int i = 0; i < Global.me.photoList.Count; i++)
        {
            if (i == galleryNum)
            {
                Global.me.photoList[i].SetActive(true);
            }
            else
            {
                Global.me.photoList[i].SetActive(false);
            }
        }
        
    }
}
