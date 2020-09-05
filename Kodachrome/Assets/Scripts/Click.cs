using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{

    public AudioSource winding;
    public AudioSource shutter;
    
    [SerializeField] private string photoFolder;

    public int numOfPhotos = 16;

    public Camera camera;
    public bool wound;
    public float woundTimer;
    public float woundTime = .35f;

    public Texture lastPhoto;
    
    private Photo m_currentPhoto;
    
    [SerializeField] private GameObject photoPrefab;
    [SerializeField] private GameObject gallery;
    // Start is called before the first frame update
    void Start()
    {
        wound = true;
        camera = this.GetComponent<Camera>();
        woundTimer = woundTime;
    }

    // Update is called once per frame
    void Update()
    {
        if (numOfPhotos == 0)
        {
            Global.me.done = true;
        }
        if (Global.me.gameState == Global.GameState.Game)
        {
            if (Input.GetKey(KeyCode.Return) && wound == false)
            {
                woundTimer -= Time.deltaTime;
            }else if (Input.GetKeyUp(KeyCode.Space) && wound)
            {
                wound = false;
                TakePicture();
                shutter.PlayOneShot(shutter.clip);
            }

            if (Input.GetKeyDown(KeyCode.Return) && wound == false)
            {
                winding.PlayOneShot(winding.clip);
            }
            
            if (woundTimer < 0)
            {
                wound = true;
                woundTimer = woundTime;
            }
        
        
            

            if (wound == false)
            {
                Global.me.canTake = false;
            }
            else
            {
                Global.me.canTake = true;
            }
        }
        
    }
    
    public void TakePicture()
    {
        Texture2D texture;

        if (!Directory.Exists(photoFolder))
        {
            Directory.CreateDirectory(photoFolder);
        }
        
        texture = TakeSnapshot(photoFolder + "/" + numOfPhotos + ".png");
        numOfPhotos--;
        
        AddPhoto(texture);
        
    }

    
    
    public Texture2D TakeSnapshot(string _path)
    {
        // capture the virtuCam and save it as a square PNG.

        //why can't it save it save the size properly? ugh fuck you have to scale it by the lens and only capture that area
        int sqr = 512;
        int width = Screen.width;
        int height = Screen.height;

        //m_Camera.aspect = 1.0f;
        // recall that the height is now the "actual" size from now on

        RenderTexture tempRT = new RenderTexture(width, height, 24);
        // the 24 can be 0,16,24, formats like
        // RenderTextureFormat.Default, ARGB32 etc.

        camera.targetTexture = tempRT;
        camera.Render();

        RenderTexture.active = tempRT;
        Texture2D virtualPhoto = new Texture2D(width, height, TextureFormat.RGB24, false);
        // false, meaning no need for mipmaps
        virtualPhoto.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        virtualPhoto.Apply();

        //Texture2D returnPhoto = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
        Texture2D returnPhoto = new Texture2D(width, height, TextureFormat.RGB24, false);
        
        returnPhoto.ReadPixels(new Rect(0, 0, width, height), 0, 0);
        //returnPhoto.ReadPixels(new Rect(width/2-sqr/2, height/2-sqr/2, sqr, sqr), 0, 0);
        returnPhoto.Apply();

        RenderTexture.active = null; //can help avoid errors 
        camera.targetTexture = null;

        Destroy(tempRT);

        SavePNGFromTexture(virtualPhoto, _path);

        return returnPhoto;
    }
    
    void AddPhoto(Texture2D texture) //saving to the list of photos
    {
        
        //adds gameobject to gameobject list. list of pictures stored somewhere else
        GameObject temp = Instantiate(photoPrefab, gallery.transform);
        
        //Photo temp = _base.GetComponent<Photo>();
        lastPhoto = texture;
        temp.GetComponent<RawImage>().texture = texture;
        Global.me.photoList.Add(temp); 
        
        //sp.texture = texture;
    } 
    
    public void SavePNGFromTexture(Texture2D texture, string path)
    {
        byte[] bytes;
        bytes = texture.EncodeToPNG();

        File.WriteAllBytes(path, bytes);
    }
}
