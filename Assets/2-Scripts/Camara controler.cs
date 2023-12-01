using System.Collections;
using UnityEngine;

public class Camaracontroler : MonoBehaviour
{
    //Velocidade de rota��o da camara;
    [SerializeField] private float rotationSpeed;
    public GameObject[] camPositions;
    public GameObject[] puzzlePositions;
    public Transform lastPosition;
    public GameObject currentPosition;
    private int currentIndexPosition = 0;
    public bool canRotate = true;
    public bool isInPuzzle = false;
    private Quaternion targetRotation;

    public void Start()
    {
        currentPosition = Camera.main.gameObject;
        lastPosition = camPositions[0].transform;
    }
    public void GoToPos(Transform position)
    {
        if (position != null)
        {
            Debug.Log(position);
            //lastPosition = transform;
            StartCoroutine(GoPuzzle(position));            
        }
    }
    public void GoToBack(Transform position)
    {
        if (position != null)
        {
            Debug.Log(position);
            //lastPosition = transform;
            StartCoroutine(GoBack(position));
        }
    }
    IEnumerator GoBack(Transform puzztrans)
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
        canRotate = true;
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
        isInPuzzle = true;
    }

    //� chamado quando o jogador faz o gesto de rodar para a esquerda
    public void RotateLeft()
    {
        StartCoroutine(RotateCameraNegative90Degrees());        
    }

    //� chamado quando o jogador faz o gesto de rodar para a direita
    public void RotateRight()
    {
        StartCoroutine(RotateCamera90Degrees());        
    }
    IEnumerator RotateCamera90Degrees()
    {
        if (canRotate)
        {
            if (currentIndexPosition == 0)
            {
                currentIndexPosition = camPositions.Length - 1;
                targetRotation = Quaternion.Euler(camPositions[currentIndexPosition].transform.localEulerAngles);
            }
            else
            {
                currentIndexPosition -= 1;
                targetRotation = Quaternion.Euler(camPositions[currentIndexPosition].transform.localEulerAngles);
            }            
            canRotate = false;
        }        
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {            
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation;
        canRotate = true;
    }

    IEnumerator RotateCameraNegative90Degrees()
    {
        if (canRotate)
        {
            if (currentIndexPosition == camPositions.Length - 1)
            {
                currentIndexPosition = 0;
                targetRotation = Quaternion.Euler(camPositions[currentIndexPosition].transform.localEulerAngles);
            }
            else
            {
                currentIndexPosition += 1;
                targetRotation = Quaternion.Euler(camPositions[currentIndexPosition].transform.localEulerAngles);
            }
            canRotate = false;
        }        
        while (Quaternion.Angle(transform.rotation, targetRotation) > 0.01f)
        {
            transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        transform.rotation = targetRotation;
        canRotate = true;
    }
}