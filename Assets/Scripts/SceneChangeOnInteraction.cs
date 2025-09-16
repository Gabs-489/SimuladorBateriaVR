using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.XR.Interaction.Toolkit;
using UnityEngine.XR.Interaction.Toolkit.Interactables;
using UnityEngine.XR.Interaction.Toolkit.Interactors;


public class SceneChangeOnInteraction : MonoBehaviour
{
        [Header("Loading UI")]
    public GameObject loadingPanel;   // Panel de carga (Canvas)
    public Slider progressBar;        // Barra de progreso
    public Text progressText;  
     public string sceneName;         // Texto opcional (%)


    // 👇 Para XR Interactions
    private XRBaseInteractable interactable;

    void Awake()
    {
        // Si este objeto tiene un XR Interactable, lo conectamos
        interactable = GetComponent<XRBaseInteractable>();
        if (interactable != null)
        {
            interactable.selectEntered.AddListener(OnSelected);
        }
    }

    // 📌 Llamado desde botón de UI
    public void LoadSceneFromUI()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartLoading(sceneName);
        }
    }

    // 📌 Llamado cuando se selecciona el objeto en VR
    private void OnSelected(SelectEnterEventArgs args)
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            StartLoading(sceneName);
        }
    }


    // 🔹 Lógica compartida
    private void StartLoading(string targetScene)
    {
        loadingPanel.SetActive(true); // Mostrar UI
        StartCoroutine(LoadAsynchronously(targetScene));
    }

    IEnumerator LoadAsynchronously(string targetScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false; // Controlamos activación

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null) progressBar.value = progress;
            if (progressText != null) progressText.text = (progress * 100f).ToString("F0") + "%";

            // Cuando llega a 90% ya está lista
            if (operation.progress >= 0.9f)
            {
                // 🔹 Aquí decides si cargas automático o esperas confirmación
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}

