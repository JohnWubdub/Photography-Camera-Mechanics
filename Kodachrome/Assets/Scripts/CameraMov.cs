using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraMov : MonoBehaviour
{
    private Camera camera;
    
    public float wideAngle = 48.67f;
    public float teleAngle = 160.07f;
    public float midAngle = 104.37f;
    
    public Vector3 widePos = new Vector3(0,1, -10);

    public float movSpeed = .3f;
    public float midSpeed = .5f;
    public float teleSpeed = .7f;

    //top
    public float topBound1;
    public float topBound2;
    
    //bottom
    public float bottomBound1;
    public float bottomBound2;
    
    //left
    public float leftBound1;
    public float leftBound2;
    
    //right
    public float rightBound1;
    public float rightBound2;


    void Start()
    {
        camera = this.GetComponent<Camera>();
    }

    void Update()
    {
        if (Global.me.gameState == Global.GameState.Game)
        {
            if (Global.me.cameraState != Global.CameraState.wide)
            {
                CameraMovingCntrls(); //also has photo library tab control
                SpeedCheck();
                Bounds();
            }
            else
            {
                camera.transform.position = widePos;
            }
        
            LensCntrls();
            Zoomer(); 
        }
    }


    
    
    //camera moving 
    void Bounds()
    {
        if (Global.me.cameraState == Global.CameraState.midrange)
        {
            MidBounds();
        }
        else
        {
            TeleBounds();
        }
    }

    void MidBounds()
    {
        if (transform.position.x > rightBound1)
        {
            transform.position = new Vector3(rightBound1,transform.position.y,transform.position.z);
        }
        if (transform.position.x < leftBound1)
        {
            transform.position = new Vector3(leftBound1,transform.position.y,transform.position.z);
        }
        if (transform.position.y < bottomBound1)
        {
            transform.position = new Vector3(transform.position.x,bottomBound1,transform.position.z);
        }
        if (transform.position.y > topBound1)
        {
            transform.position = new Vector3(transform.position.x,topBound1,transform.position.z);
        }
    }
    void TeleBounds()
    {
        if (transform.position.x > rightBound2)
        {
            transform.position = new Vector3(rightBound2,transform.position.y,transform.position.z);
        }
        if (transform.position.x < leftBound2)
        {
            transform.position = new Vector3(leftBound2,transform.position.y,transform.position.z);
        }
        if (transform.position.y < bottomBound2)
        {
            transform.position = new Vector3(transform.position.x,bottomBound2,transform.position.z);
        }
        if (transform.position.y > topBound2)
        {
            transform.position = new Vector3(transform.position.x,topBound2,transform.position.z);
        }
    }

    void CameraMovingCntrls()
    {
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            transform.position = new Vector3(transform.position.x - (movSpeed * Time.deltaTime) , transform.position.y ,transform.position.z);
        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            transform.position = new Vector3(transform.position.x + (movSpeed * Time.deltaTime) , transform.position.y ,transform.position.z);
        }
        if (Input.GetKey(KeyCode.UpArrow))
        {
            transform.position = new Vector3(transform.position.x  , transform.position.y + (movSpeed * Time.deltaTime) ,transform.position.z);
        }
        if (Input.GetKey(KeyCode.DownArrow))
        {
            transform.position = new Vector3(transform.position.x  , transform.position.y - (movSpeed * Time.deltaTime) ,transform.position.z);
        }
        
    }

    void SpeedCheck()
    {
        if (Global.me.cameraState == Global.CameraState.midrange)
        {
            movSpeed = midSpeed;
        }

        if (Global.me.cameraState == Global.CameraState.telephoto)
        {
            movSpeed = teleSpeed;
        }
    }



    
    
    //lens

    void LensCntrls()
    {
        if (Input.GetKeyUp(KeyCode.Equals) || Input.GetKeyUp(KeyCode.KeypadPlus))// zoom in
        {
            if (Global.me.cameraState == Global.CameraState.midrange)
            {
                Global.me.cameraState = Global.CameraState.telephoto;
            } 
            else if (Global.me.cameraState == Global.CameraState.wide)
            {
                Global.me.cameraState = Global.CameraState.midrange;
            }
        }
        if (Input.GetKeyUp(KeyCode.Minus) || Input.GetKeyUp(KeyCode.KeypadMinus)) //zoom out
        {
            if (Global.me.cameraState == Global.CameraState.midrange)
            {
                Global.me.cameraState = Global.CameraState.wide;
            } 
            else if (Global.me.cameraState == Global.CameraState.telephoto)
            {
                Global.me.cameraState = Global.CameraState.midrange;
            }
        }
        
    }
    
    void Zoomer()
    {
        if (Global.me.cameraState == Global.CameraState.wide)
        {
            camera.focalLength = wideAngle;
        } 
        
        if (Global.me.cameraState == Global.CameraState.telephoto)
        {
            camera.focalLength = teleAngle;
        } 
        
        if (Global.me.cameraState == Global.CameraState.midrange)
        {
            camera.focalLength = midAngle;
        } 
    }
}
