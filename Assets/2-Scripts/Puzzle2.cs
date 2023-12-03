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
        if (currentSlot == 4 && currentRotation != 4)
        {
            slots[4] = false;
        }
        else if(currentSlot == 2)
        {
            slots[4] = true;
        }
        if (currentSlot == 3 && currentRotation != 4)
        {
            slots[3] = false;
        }
        else if(currentSlot == 2)
        {
            slots[3] = true;
        }
        if (currentSlot == 2 && currentRotation != 4)
        {
            slots[2] = false;
        }
        else if(currentSlot == 2)
        {
            slots[2] = true;
        }
        if (currentSlot == 1 && currentRotation != 2)
        {
            slots[1] = false;
        }
        else if(currentSlot == 1)
        {
            slots[1] = true;
        }
        if (currentSlot == 0 && currentRotation != 3)
        {
            slots[0] = false;
        }
        else if(currentSlot == 0)
        {
            slots[0] = true;
        }



        if (slots[0] == true && slots[1] == true && slots[2] == true && slots[3] == true && slots[4] == true)
        {
            // Open Drawer Animation
        }
    }

    void RotateObject()
    {
        if (handInputHandler.hDetectedState == 1f)//1 is Left
        {
            StartCoroutine(RotateSlot90Degrees());
        }
        else if (handInputHandler.hDetectedState == 2f)//2 is right
        {
            StartCoroutine(RotateSlotNegative90());
        }
        else if (handInputHandler.hDetectedState == 4f)
        {
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
