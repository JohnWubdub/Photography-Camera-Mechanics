using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.Video;
using Debug = UnityEngine.Debug;

public class FocusPoints : MonoBehaviour
{
    public float diss;
    public float dissing;
    
    public GameObject point1;
    public GameObject point3;

    public GameObject wideCollider;
    public GameObject midCollider;
    public GameObject teleCollider;
    
    public float[] p1TimeBounds = new float[2];
    public float[] p2TimeBounds = new float[2];
    public float[] p3TimeBounds = new float[2];
    
    public Vector3[] points3;

    public float point1MaxDiss;
    public float point3MaxDiss;

    // Update is called once per frame
    void Update()
    {
        if (Global.me.gameState == Global.GameState.Game)
        {
            LensLayers();
            Point1Logic();
            Point2Logic();
            Point3Logic();
        }
        
        diss = Mathf.Abs(Vector3.Distance(midCollider.transform.position, point1.transform.position));
        dissing = Mathf.Abs(Vector3.Distance(teleCollider.transform.position, point3.transform.position));
    }

    void LensLayers() 
    {
        if (Global.me.cameraState == Global.CameraState.wide)
        {
            wideCollider.SetActive(true);
            midCollider.SetActive(false);
            teleCollider.SetActive(false);
        } else if (Global.me.cameraState == Global.CameraState.midrange)
        {
            wideCollider.SetActive(false);
            midCollider.SetActive(true);
            teleCollider.SetActive(false);
        } else if (Global.me.cameraState == Global.CameraState.telephoto)
        {
            wideCollider.SetActive(false);
            midCollider.SetActive(false);
            teleCollider.SetActive(true);
        }
    }
    
    //Point 1 _______________________________________________________________________________________________________________________________________________
    void Point1Logic() //activation and lerping (if needed)
    {
        if (Global.me.cameraState == Global.CameraState.telephoto && (Global.me.videoTime >= p1TimeBounds[0] && Global.me.videoTime <= p1TimeBounds[1]))
        {
            Point1Check(); //checking if picture is taken and colliding
        }
    }
    void Point1Check() 
    {
        if (Distancing(point1.transform.position, teleCollider.transform.position, point1MaxDiss) && Input.GetKey(KeyCode.Space)&& Global.me.canTake)
        {
            Global.me.focalP1 = true;
        }
    }
    
    
    //point 2 _______________________________________________________________________________________________________________________________________________

    void Point2Logic()
    {
        if (Global.me.cameraState == Global.CameraState.wide && (Global.me.videoTime >= p2TimeBounds[0] && Global.me.videoTime <= p2TimeBounds[1]))
        {
            Point2Check(); //checking if picture is taken and colliding
        }
    }
    void Point2Check()
    {
        if (Input.GetKey(KeyCode.Space) && Global.me.canTake)
        {
            Global.me.focalP2 = true;
        }
    }
    
    
    //point 3 _________________________________________________________________________________________________________________________________________________
    
    void Point3Logic()
    {
        if (Global.me.cameraState == Global.CameraState.midrange && (Global.me.videoTime >= p3TimeBounds[0] && Global.me.videoTime <= p3TimeBounds[1]))
        {
            Point3Check(); 
        }
        Point3Move();
    }
    void Point3Check()
    {
        if (Distancing(point3.transform.position, midCollider.transform.position, point3MaxDiss) && Input.GetKey(KeyCode.Space) && Global.me.canTake)
        {
            Global.me.focalP3 = true;
        }
    }
    void Point3Move()
    {
        if (Global.me.videoTime >= p3TimeBounds[0] && Global.me.videoTime <= p3TimeBounds[1])
        {
            point3.transform.position = Lerping( points3[0], points3[1], (p3TimeBounds[1] - p3TimeBounds[0]));
        }
        else
        {
            point3.transform.position = points3[0];
        }
    }
    
    
    
    
    
    
    
    
    
    Vector3 Lerping( Vector3 startPos, Vector3 endPos, float time) 
    {
        float timeSinceStarted = (float)Global.me.videoTime - p3TimeBounds[0];

        float percentageComplete = timeSinceStarted / (p3TimeBounds[1] - p3TimeBounds[0]);

        var result = Vector3.Lerp(startPos, endPos, percentageComplete);

        return result;
    }

    public bool Distancing(Vector3 point, Vector3 center, float maxDiss) //is it close enough?
    {
         diss = Mathf.Abs(Vector3.Distance(center, point));
        if (diss <= maxDiss)
        {
            return true;
        }else
        {
            return false;
        }
    }
    
}
