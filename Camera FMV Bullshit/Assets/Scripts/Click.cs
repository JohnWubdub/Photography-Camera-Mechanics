using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using UnityEngine.UI;

public class Click : MonoBehaviour
{
    [SerializeField] private string m_photoFolder;
    [SerializeField] private string m_photoFolderName = "TempPics";
    [SerializeField] private string m_albumFolder;
    [SerializeField] private string m_albumFolderName = "AlbumPics";
    [SerializeField] private int m_maxNumOfPhotos = 60;
    [SerializeField] private int m_minNumOfPhotos = 0;
    [SerializeField] private bool m_usingScreenCapture = false;
    [SerializeField] private int m_numOfPhotos;
    [SerializeField] private Text m_numPhotoText;
    public Camera camera;
    
    
    private Snap_Photo m_currentPhoto;
    
    [Header("~ Saved Photos List ~"), Space(10)]
    [SerializeField] private GameObject m_blackScreen;
    [SerializeField] private List<Snap_Photo> m_photoList;
    [SerializeField] private GameObject m_photoBase;
    [SerializeField] private GameObject m_evaluatePhotoPanel;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyUp(KeyCode.Space))
        {
            TakePicture();
        }
    }
    
    public void TakePicture()
    {
        Texture2D texture;

        if (!Directory.Exists(m_photoFolder))
        {
            Directory.CreateDirectory(m_photoFolder);
        }
        
        texture = TakeSnapshot(m_photoFolder + "/" + m_numOfPhotos + ".png");
        m_numOfPhotos++;
        
        SnapEvaluationData(texture);
        
    }

    
    
    public Texture2D TakeSnapshot(string _path)
    {
        // capture the virtuCam and save it as a square PNG.

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

        Texture2D returnPhoto = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
        returnPhoto.ReadPixels(new Rect(width/2-sqr/2, height/2-sqr/2, sqr, sqr), 0, 0);
        returnPhoto.Apply();

        RenderTexture.active = null; //can help avoid errors 
        camera.targetTexture = null;

        Destroy(tempRT);

        SavePNGFromTexture(virtualPhoto, _path);

        return returnPhoto;
    }
    
    void SnapEvaluationData(Texture2D texture)
    {
        GameObject _base = Instantiate(m_photoBase, m_evaluatePhotoPanel.transform);
        Snap_Photo temp = _base.GetComponent<Snap_Photo>();

        RawImage sp = _base.GetComponent<RawImage>();
        sp.texture = texture;
    }
    
    public void SavePNGFromTexture(Texture2D texture, string path)
    {
        byte[] bytes;
        bytes = texture.EncodeToPNG();

        File.WriteAllBytes(path, bytes);
    }
}
