using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputHandler : MonoBehaviour
{
    public Camaracontroler camControler;
    public float hDetectedState = 0;

    public void OnFistDetected(OpenCvSharp.Rect Rect)
    {
        if (camControler.isInPuzzle && !camControler.canRotate)
        {
            camControler.GoToPos(camControler.lastPosition);
            camControler.isInPuzzle = false;
            camControler.canRotate = true;

        }
    }
    public void OnLeftDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            camControler.RotateLeft();
        }

        hDetectedState = -1f;        
    }
    public void OnRightDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            camControler.RotateRight();
        }

        hDetectedState = 1f;        
    }
    public void OnLeftPalmDetected(OpenCvSharp.Rect Rect)
    {
        
    }
    public void OnRightPalmDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle && camControler.canRotate)
        {
            for (int i = 0; i < camControler.camPositions.Length; i++)
            {
                if (camControler.currentPosition.transform.rotation == camControler.camPositions[i].transform.rotation)
                {

                    camControler.GoToPos(camControler.puzzlePositions[i].transform);
                    camControler.isInPuzzle = true;
                }
            }            
        }
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
