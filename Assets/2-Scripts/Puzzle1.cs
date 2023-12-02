using UnityEngine;
using UnityEngine.Animations;

public class Puzzle1 : MonoBehaviour
{
    public float rotationSpeed = 10;
    public HandInputHandler handInputHandler;
    public Animator labAnimator;
    public AudioSource drawerAudio;
    //public bool isPuzzleDone;


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
            transform.Rotate(Vector3.right, -rotationSpeed * Time.deltaTime);
        }
        else if (handInputHandler.hDetectedState == 2f)
        {
            //+ Z foward
            transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
        }

        //transform.Rotate(Vector3.forward, rotationSpeed * Time.deltaTime);

        if (handInputHandler.hDetectedState != 0)
        {
            handInputHandler.hDetectedState = 0;
        }

        
        //transform.Rotate(Vector3.right, rotationSpeed * Time.deltaTime);
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Puzzle1ball")
        {
            //Play Animation
            labAnimator.SetTrigger("opendrawer");
            drawerAudio.Play();
        }
    }
}