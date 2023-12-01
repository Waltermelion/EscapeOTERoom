using OpenCvSharp;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandInputHandler : MonoBehaviour
{
    public Camaracontroler camControler;
    public float hDetectedState = 0;
    public int currentPuzzle = 0;

    public void OnFistDetected(OpenCvSharp.Rect Rect)
    {
        if (camControler.isInPuzzle && !camControler.canRotate)
        {
            camControler.isInPuzzle = false;
            camControler.GoToBack(camControler.lastPosition);            
        }
    }
    public void OnLeftDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            //apontar para a direita, rotate right
            camControler.RotateRight();
        }

        hDetectedState = 1f;        
    }
    public void OnRightDetected(OpenCvSharp.Rect Rect)
    {
        if (!camControler.isInPuzzle)
        {
            //apontar para a esquerda, rotate left
            camControler.RotateLeft();
        }

        hDetectedState = 2f;        
    }
    public void OnLeftPalmDetected(OpenCvSharp.Rect Rect)
    {
        Debug.Log("Left Palm Detected");
        hDetectedState = 3f;
    }
    public void OnRightPalmDetected(OpenCvSharp.Rect Rect)
    {
        hDetectedState = 4f;
        if (!camControler.isInPuzzle && camControler.canRotate)
        {
            camControler.canRotate = false;
            for (int i = 0; i < camControler.camPositions.Length; i++)
            {
                if (camControler.currentPosition.transform.localEulerAngles == camControler.camPositions[i].transform.localEulerAngles)
                {
                    camControler.lastPosition = camControler.camPositions[i].transform;
                    camControler.GoToPos(camControler.puzzlePositions[i].transform);                    
                    camControler.canRotate = false;
                    currentPuzzle = i;
                }
            }            
        }
    }
}
