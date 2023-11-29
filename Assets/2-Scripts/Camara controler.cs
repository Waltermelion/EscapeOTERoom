using System.Collections;
using UnityEngine;

public class Camaracontroler : MonoBehaviour
{
    //Velocidade de rotação da camara;
    [SerializeField] private float rotationSpeed;
    public GameObject[] camPositions;
    public GameObject[] puzzlePositions;
    private GameObject lastPosition;
    public GameObject currentPosition;
    public bool canRotate = true;
    public bool isInPuzzle = false;
    private Quaternion targetRotation;

    public void Start()
    {
        currentPosition = Camera.main.gameObject;
    }
    public void GoToPuzzle(GameObject puzzlepos)
    {
        if (puzzlepos != null)
        {
            StartCoroutine(GoPuzzle(puzzlepos.transform));
        }
    }
    IEnumerator GoPuzzle(Transform puzztrans)
    {
        
        while (Vector3.Distance(transform.position, puzztrans.position) > 0.01f)
        {
            //Vector3 directionToPuzzle = puzztrans.position - transform.position;
            Vector3 newPosition = Vector3.Slerp(transform.position, puzztrans.position, rotationSpeed * Time.deltaTime);
            transform.rotation = Quaternion.Slerp(transform.rotation, puzztrans.rotation, rotationSpeed * Time.deltaTime);
            transform.position = newPosition;
            yield return null;
        }
        transform.position = puzztrans.position;
        transform.rotation = puzztrans.rotation;
    }

    //É chamado quando o jogador faz o gesto de rodar para a esquerda
    public void RotateLeft()
    {
        StartCoroutine(RotateCameraNegative90Degrees());        
    }

    //É chamado quando o jogador faz o gesto de rodar para a direita
    public void RotateRight()
    {
        StartCoroutine(RotateCamera90Degrees());        
    }
    IEnumerator RotateCamera90Degrees()
    {
        if (canRotate)
        {
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y + 90, 0);
            canRotate = false;
        }        
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        canRotate = true;
    }

    IEnumerator RotateCameraNegative90Degrees()
    {
        if (canRotate)
        {
            targetRotation = Quaternion.Euler(0, transform.eulerAngles.y - 90, 0);
            canRotate = false;
        }        
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        canRotate = true;
    }
}