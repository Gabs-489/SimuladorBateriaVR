using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CanvaSwitcher : MonoBehaviour
{
    public GameObject canvasToHide;
    public GameObject canvasToShow;

    public void SwitchCanvas()
    {
        if (canvasToHide != null) canvasToHide.SetActive(false);
        if (canvasToShow != null) canvasToShow.SetActive(true);
    }
}
