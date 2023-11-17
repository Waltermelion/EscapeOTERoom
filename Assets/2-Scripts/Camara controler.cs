using DG.Tweening;
using UnityEngine;

public class Camaracontroler : MonoBehaviour
{
    [SerializeField] private GameObject mainCameraObject;
    [SerializeField] private float speed;


    public void RotateLeft()
    {

        //mainCameraObject.transform.Rotate(0, Time.deltaTime * (-speed), 0);

        //mainCameraObject.transform.rotation = new Quaternion(Mathf.Lerp(0,90,(Time.deltaTime)),0,0,0);

        mainCameraObject.transform.DORotate(new Vector3(0,mainCameraObject.transform.rotation.y - 90, 0), 1f);
        
    }

    public void RotateRight()
    {
        mainCameraObject.transform.DORotate(new Vector3(0, mainCameraObject.transform.rotation.y + 90, 0), 1f);
    }
}