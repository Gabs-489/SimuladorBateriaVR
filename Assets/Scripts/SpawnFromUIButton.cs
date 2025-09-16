using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnFromUIButton : MonoBehaviour
{
    public GameObject prefabInScene;  // Prefab ya colocado en la escena
    private bool isVisible = false;

    // Método que se llama desde el botón
    public void TogglePrefab()
    {
        if (prefabInScene != null)
        {
            // Cambiar visibilidad
            isVisible = !isVisible;
            prefabInScene.SetActive(isVisible);
        }
    }
}
