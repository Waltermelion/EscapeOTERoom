using DG.Tweening;
using System.Collections;
using UnityEngine;

public class Camaracontroler : MonoBehaviour
{
    [SerializeField] private GameObject mainCameraObject;
    [SerializeField] private float rotationSpeed;


    public void RotateLeft()
    {
        StartCoroutine(RotateCameraNegative90Degrees());
        //Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);

        //mainCameraObject.transform.Rotate(0, Time.deltaTime * (-speed), 0);

        //mainCameraObject.transform.rotation = new Quaternion(Mathf.Lerp(0,90,(Time.deltaTime)),0,0,0);

        //mainCameraObject.transform.DORotate(new Vector3(0,mainCameraObject.transform.rotation.y - 90, 0), 1f);
        
    }

    public void RotateRight()
    {
        StartCoroutine(RotateCamera90Degrees());
        //Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
        //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
        //mainCameraObject.transform.DORotate(new Vector3(0, mainCameraObject.transform.rotation.y + 90, 0), 1f);
    }
    IEnumerator RotateCamera90Degrees()
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }

    IEnumerator RotateCameraNegative90Degrees()
    {
        Quaternion targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
    }
}