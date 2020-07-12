using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Manager : MonoBehaviour
{
    /*
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

            m_Camera.targetTexture = tempRT;
            m_Camera.Render();

            RenderTexture.active = tempRT;
            Texture2D virtualPhoto = new Texture2D(width, height, TextureFormat.RGB24, false);
            // false, meaning no need for mipmaps
            virtualPhoto.ReadPixels(new Rect(0, 0, width, height), 0, 0);
            virtualPhoto.Apply();

            Texture2D returnPhoto = new Texture2D(sqr, sqr, TextureFormat.RGB24, false);
            returnPhoto.ReadPixels(new Rect(width/2-sqr/2, height/2-sqr/2, sqr, sqr), 0, 0);
            returnPhoto.Apply();

            RenderTexture.active = null; //can help avoid errors 
            m_Camera.targetTexture = null;

            Destroy(tempRT);

            SavePNGFromTexture(virtualPhoto, _path);

            return returnPhoto;
        }

        /// <summary>
        /// Method that saves a png based on png and render to texture
        /// </summary>
        /// <param name="texture"></param>
        /// <param name="path"></param>
        public void SavePNGFromTexture(Texture2D texture, string path)
        {
            byte[] bytes;
            bytes = texture.EncodeToPNG();

            File.WriteAllBytes(path, bytes);
        }

        /// <summary>
        /// We need to create the photo folder if it's not in the game data
        /// </summary>
        void CreatePhotoFolder()
        {
            m_photoFolder = Application.dataPath + "/Resources/" + m_photoFolderName;
            if (!Directory.Exists(m_photoFolder))
            {
                Directory.CreateDirectory(m_photoFolder);
            }
        }
        /// <summary>
        /// We need to create the album folder if it's not in the game data
        /// </summary>
        void CreateAlbumFolder()
        {
            m_albumFolder = Application.dataPath + "/Resources/" + m_albumFolderName;
            if (!Directory.Exists(m_albumFolder))
            {
                Directory.CreateDirectory(m_albumFolder);

                if (!Directory.Exists(m_albumFolder + "/" + m_stage))
                {
                    Directory.CreateDirectory(m_albumFolder + "/" + m_stage);
                }
            }
        }
        #endregion
        */
}
