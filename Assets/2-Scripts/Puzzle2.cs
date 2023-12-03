using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Puzzle2 : MonoBehaviour
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
    public Animator quadroAnimator;
    public AudioSource drawerAudio;
    public AudioClip rotatingWood;
    public AudioClip completionSound;
    public AudioClip changeSelectedAudio;
    private bool drawerAudioPlayed = false;

    void Update()
    {
        if (slotCanRotate)
        {
            if (handInputHandler.camControler.isInPuzzle && handInputHandler.currentPuzzle == 1)
            {
                RotateObject();
            }
        }
        CheckSlotRotation();
    }
    void CheckSlotRotation()
    {
        if (currentSlot == 4 && currentRotation == 1)
        {
            slots[4] = true;
        }
        if (currentSlot == 3 && currentRotation == 3)
        {
            slots[3] = true;
        }
        if (currentSlot == 2 && currentRotation == 5)
        {
            slots[2] = true;
        }
        if (currentSlot == 1 && currentRotation == 2)
        {
            slots[1] = true;
        }
        if (currentSlot == 0 && currentRotation == 4)
        {
            slots[0] = true;
        }



        if (slots[0] == true && slots[1] == true && slots[2] == true && slots[3] == true && slots[4] == true)
        {
            // Open Drawer Animation
            if (!drawerAudioPlayed)
            {
                drawerAudio.Play();
                drawerAudio.PlayOneShot(completionSound);                
                drawerAudioPlayed = true;
            }
            quadroAnimator.SetTrigger("opendrawer");
        }
    }

    void RotateObject()
    {
        if (handInputHandler.hDetectedState == 1f)//1 is Left
        {
            drawerAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlot90Degrees());
        }
        else if (handInputHandler.hDetectedState == 2f)//2 is right
        {
            drawerAudio.PlayOneShot(rotatingWood);
            StartCoroutine(RotateSlotNegative90());
        }
        else if (handInputHandler.hDetectedState == 4f)
        {
            drawerAudio.PlayOneShot(changeSelectedAudio);
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
        while (Quaternion.Angle(slotsGmo[currentSlot].transform.localRotation, targetRotation) > 0.01f)
        {
            slotsGmo[currentSlot].transform.localRotation = Quaternion.Slerp(slotsGmo[currentSlot].transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        slotsGmo[currentSlot].transform.localRotation = targetRotation;
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
        while (Quaternion.Angle(slotsGmo[currentSlot].transform.localRotation, targetRotation) > 0.01f)
        {
            slotsGmo[currentSlot].transform.localRotation = Quaternion.Slerp(slotsGmo[currentSlot].transform.localRotation, targetRotation, rotationSpeed * Time.deltaTime);
            yield return null;
        }
        slotsGmo[currentSlot].transform.localRotation = targetRotation;
        slotCanRotate = true;
    }
}
