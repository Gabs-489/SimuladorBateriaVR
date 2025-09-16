using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem; // 👈 new input system

[RequireComponent(typeof(AudioSource))]
public class PlaySound : MonoBehaviour
{
    private AudioSource source;

    [Header("Button Trigger Settings")]
    public bool playOnButtonPress = false;

    // Instead of string "A" or "B", expose input actions
    public InputActionProperty buttonSouth; // A (right) / X (left)
    public InputActionProperty buttonEast;  // B (right) / Y (left)

    void Start()
    {
        source = GetComponent<AudioSource>();
    }

    void Update()
    {
        if (playOnButtonPress)
        {
            CheckButtonPress();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("DrumStickHead"))
        {
            source.volume = other.gameObject.GetComponent<TrackSpeed>().speed;
            ActivateSound();
        }
    }

    private void ActivateSound()
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    void CheckButtonPress()
    {
        // Instead of switch on string, check input actions
        if (buttonSouth.action != null && buttonSouth.action.WasPressedThisFrame())
        {
            ActivateSound(); // A / X pressed
        }

        if (buttonEast.action != null && buttonEast.action.WasPressedThisFrame())
        {
            ActivateSound(); // B / Y pressed
        }
    }
}
