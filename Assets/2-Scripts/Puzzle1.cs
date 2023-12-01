using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public float rotationSpeed = 10f;
    public HandInputHandler handInputHandler;

    void Update()
    {
        if (handInputHandler.camControler.isInPuzzle && handInputHandler.currentPuzzle == 0)
        {
            RotateObject();
        }        
    }

    void RotateObject()
    {
        if (handInputHandler.hDetectedState == 1f)
        {
            //- Z foward
            transform.Rotate(Vector3.forward, -rotationSpeed * Time.deltaTime);
        }
        else if (handInputHandler.hDetectedState == 2f)
        {
            //+ Z foward
            transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);
        }
        else if (handInputHandler.hDetectedState == 3f)
        {
            //- X right
            transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
        }
        else if (handInputHandler.hDetectedState == 4f)
        {
            //+ X right
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }

        //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        if (handInputHandler.hDetectedState != 0)
        {
            handInputHandler.hDetectedState = 0;
        }

        
        //transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
}