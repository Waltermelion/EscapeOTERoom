using UnityEngine;

public class Puzzle1 : MonoBehaviour
{
    public float rotationSpeed = 5f;
    public HandInputHandler handInputHandler;

    void Update()
    {
        RotateObject();
    }

    void RotateObject()
    {
        // Mudar isto por uma variavel nos eventos de deteção
        float horizontalInput = Input.GetAxis("Horizontal");
        transform.Rotate(Vector3.forward, handInputHandler.hDetectedState * rotationSpeed * Time.deltaTime);

        if (handInputHandler.hDetectedState != 0)
        {
            handInputHandler.hDetectedState = 0;
        }

        // Mudar isto por uma variavel nos eventos de deteção
        float verticalInput = Input.GetAxis("Vertical");
        transform.Rotate(Vector3.right, -verticalInput * rotationSpeed * Time.deltaTime);
    }
}