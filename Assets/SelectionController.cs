using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 👈 new input system

public class SelectionController : MonoBehaviour
{
    [Header("References")]
    public GameObject rightHand;          // XR Controller Right
    public GameObject selectionPointer;   // Visual pointer

    [Header("Input Actions")]
    public InputActionProperty rightTrigger; // Right index trigger

    private int layerMask;

    void Start()
    {
        // Only interact with UI layer
        layerMask = LayerMask.GetMask("UI");
    }

    void Update()
    {
        selectionPointer.SetActive(false);

        Ray ray = new Ray(rightHand.transform.position, rightHand.transform.forward);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit, 11f, layerMask))
        {
            // Show pointer at hit location
            selectionPointer.SetActive(true);
            selectionPointer.transform.position = hit.point;

            // 🔄 Replacement for OVRInput trigger check
            float triggerValue = rightTrigger.action?.ReadValue<float>() ?? 0f;

            if (triggerValue > 0.5f) // pressed beyond halfway
            {
                if (hit.collider.CompareTag("BPM"))
                {
                    hit.collider.GetComponent<ChangeSpeed>().AssignSpeed();
                }
                if (hit.collider.CompareTag("Beat"))
                {
                    hit.collider.GetComponent<NotationLoad>().ChangeBeat();
                }
            }
        }
    }
}
