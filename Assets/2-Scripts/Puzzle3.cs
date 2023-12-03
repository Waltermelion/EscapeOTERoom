using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle3 : MonoBehaviour
{
    public float rotationSpeed = 10;
    public HandInputHandler handInputHandler;
    public bool[] slots;
    public GameObject[] slotsGmo;
    public Vector3[] slotRotations;
    public int currentSlot = 0;
    public int currentRotation = 0;
    public bool slotCanRotate = true;
    private Quaternion targetRotation;
    public AudioSource relogioAudio;
    public AudioClip completionSound;
    public AudioClip changeSelectedAudio;
    public AudioClip rotatingWood;
    public GameObject clue3;
    private bool relogioAudioPlayed;

    void Update()
    {
        if (slotCanRotate)
        {
            if (handInputHandler.camControler.isInPuzzle && handInputHandler.currentPuzzle == 2)
            {
                RotateObject();
            }
        }
        CheckSlotRotation();
    }
    void CheckSlotRotation()
    {
        if (currentSlot == 1 && currentRotation != 8)
        {
            slots[1] = true;
        }
        if (currentSlot == 0 && currentRotation != 9)
        {
            slots[0] = true;
        }



        if (slots[0] == true && slots[1] == true)
        {
            if (!relogioAudioPlayed)
            {
                relogioAudio.PlayOneShot(completionSound);
                clue3.SetActive(true);
                relogioAudioPlayed = true;
            }            
        }
    }

    void RotateObject()
    {
        if (handInputHandler.hDetectedState == 1f)//1 is Left
        {
            relogioAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlot90Degrees());
        }
        else if (handInputHandler.hDetectedState == 2f)//2 is right
        {
            relogioAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlotNegative90());
        }
        else if (handInputHandler.hDetectedState == 4f)
        {
            relogioAudio.PlayOneShot(changeSelectedAudio);
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
