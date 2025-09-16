using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MultiPrefabToggle : MonoBehaviour
{
    public GameObject[] prefabsInScene; // Lista de prefabs en la escena

    // Desactiva todos al inicio
    void Start()
    {
        foreach (GameObject prefab in prefabsInScene)
        {
            if (prefab != null)
                prefab.SetActive(false);
        }
    }

    // Método para activar/desactivar un prefab según su índice en el array
    public void TogglePrefab(int index)
    {
        if (index >= 0 && index < prefabsInScene.Length && prefabsInScene[index] != null)
        {
            bool newState = !prefabsInScene[index].activeSelf;
            prefabsInScene[index].SetActive(newState);
        }
    }
}
