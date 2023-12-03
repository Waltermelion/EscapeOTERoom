using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle4 : MonoBehaviour
{
    public float rotationSpeed = 10;
    public HandInputHandler handInputHandler;
    public Animator doorAnimator;
    public AudioSource doorAudio;
    public AudioClip completionSound;
    public AudioClip rotatingWood;
    public AudioClip changeSelectedAudio;
    public GameObject winScreen;
    public bool[] slots;
    public GameObject[] slotsGmo;
    public Vector3[] slotRotations;
    public int currentSlot = 0;
    public int currentRotation = 0;
    public bool slotCanRotate = true;
    private Quaternion targetRotation;
    private bool doorAudioPlayed = false;

    void Update()
    {
        if (slotCanRotate)
        {
            if (handInputHandler.camControler.isInPuzzle && handInputHandler.currentPuzzle == 3)
            {
                RotateObject();
            }
        }
        CheckSlotRotation();
    }
    void CheckSlotRotation()
    {
        if (currentSlot == 2 && currentRotation == 4)
        {
            slots[2] = true;
        }
        if (currentSlot == 1 && currentRotation == 2)
        {
            slots[1] = true;
        }
        if (currentSlot == 0 && currentRotation == 3)
        {
            slots[0] = true;
        }

        if (slots[0] == true && slots[1] == true && slots[2] == true)
        {
            winScreen.SetActive(true);
            if (!doorAudioPlayed)
            {
                doorAudio.Play();
                doorAudio.PlayOneShot(completionSound);
                doorAudioPlayed = true;
            }
            doorAnimator.SetTrigger("opendoor");
        }
    }

    void RotateObject()
    {
        if (handInputHandler.hDetectedState == 1f)//1 is Left
        {
            doorAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlot90Degrees());
        }
        else if (handInputHandler.hDetectedState == 2f)//2 is right
        {
            doorAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlotNegative90());
        }
        else if (handInputHandler.hDetectedState == 4f)
        {
            doorAudio.PlayOneShot(changeSelectedAudio);
            if (currentSlot == slotsGmo.Length - 1)
            {
                currentSlot = 0;
            }
            else
            {
                currentSlot += 1;
            }            
        }

        if (handInputHandler.hDetectedState != 0)
        {
            handInputHandler.hDetectedState = 0;
        }
    }
    IEnumerator RotateSlot90Degrees()
    {
        if (currentRotation == 0)
        {
            currentRotation = slotRotations.Length - 1;
            targetRotation = Quaternion.Euler(slotRotations[currentRotation]);
        }
        else
        {
            currentRotation -= 1;
            targetRotation = Quaternion.Euler(slotRotations[currentRotation]);           
        }
        slotCanRotate = false;
        while (Quaternion.Angle(slotsGmo[currentSlot].transform.rotation, targetRotation) > 0.01f)
        {
            slotsGmo[currentSlot].transform.rotation = Quaternion.Slerp(slotsGmo[currentSlot].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        slotsGmo[currentSlot].transform.rotation = targetRotation;
        slotCanRotate = true;
    }

    IEnumerator RotateSlotNegative90()
    {
        if (currentRotation == slotRotations.Length - 1)
        {
            currentRotation = 0;
            targetRotation = Quaternion.Euler(slotRotations[currentRotation]);
        }
        else
        {
            currentRotation += 1;
            targetRotation = Quaternion.Euler(slotRotations[currentRotation]);
        }
        slotCanRotate = false;
        while (Quaternion.Angle(slotsGmo[currentSlot].transform.rotation, targetRotation) > 0.01f)
        {
            slotsGmo[currentSlot].transform.rotation = Quaternion.Slerp(slotsGmo[currentSlot].transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        slotsGmo[currentSlot].transform.rotation = targetRotation;
        slotCanRotate = true;
    }
}
