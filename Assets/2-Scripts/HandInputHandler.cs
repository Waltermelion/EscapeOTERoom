using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputHandler : MonoBehaviour
{
    public Camaracontroler camControler;
    public void OnFistDetected(OpenCvSharp.Rect Rect)
    {
        
    }
    public void OnLeftDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            camControler.RotateLeft();
        }        
    }
    public void OnRightDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            camControler.RotateRight();
        }        
    }
    public void OnLeftPalmDetected(OpenCvSharp.Rect Rect)
    {
        
    }
    public void OnRightPalmDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle && camControler.canRotate)
        {
            Debug.Log("First if");
            for (int i = 0; i < camControler.camPositions.Length; i++)
            {
                if (camControler.currentPosition.transform.rotation == camControler.camPositions[i].transform.rotation)
                {
                    Debug.Log("second if");
                    camControler.GoToPuzzle(camControler.puzzlePositions[i]);
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
