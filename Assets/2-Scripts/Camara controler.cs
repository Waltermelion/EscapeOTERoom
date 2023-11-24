using System.Collections;
using UnityEngine;

public class Camaracontroler : MonoBehaviour
{
    //Velocidade de rotação da camara;
    [SerializeField] private float rotationSpeed;
    [SerializeField] private GameObject[] camPositions;
    private GameObject lastpostition;

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