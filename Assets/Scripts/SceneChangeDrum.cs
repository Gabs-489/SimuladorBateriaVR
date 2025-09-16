using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;
using UnityEngine.UI;
using System.Collections;

public class SceneChangeDrum : MonoBehaviour
{   
    private AudioSource source;

    [Header("Scene Settings")]
    [Tooltip("El nombre de la escena a cargar")]
    public string sceneName;

    [Header("Button Trigger Settings")]
    public bool changeOnButtonPress = false;

    // Exponer acciones de botones (A/X, B/Y)
    public InputActionProperty buttonSouth; // A (derecha) / X (izquierda)
    public InputActionProperty buttonEast;  // B (derecha) / Y (izquierda)

    [Header("Loading UI")]
    public GameObject loadingPanel;   // Panel con imagen de fondo
    public Slider progressBar;        // Barra de progreso
    public Text progressText;         // Texto opcional (%)

    void Start()
    {
        source = GetComponent<AudioSource>();
    }
    
    private void Update()
    {
        if (changeOnButtonPress)
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
            StartLoading();
        }
    }

    private void ActivateSound()
    {
        source.pitch = Random.Range(0.8f, 1.2f);
        source.Play();
    }

    private void CheckButtonPress()
    {
        if (buttonSouth.action != null && buttonSouth.action.WasPressedThisFrame())
        {
            StartLoading();
        }

        if (buttonEast.action != null && buttonEast.action.WasPressedThisFrame())
        {
            StartLoading();
        }
    }

    private void StartLoading()
    {
        if (!string.IsNullOrEmpty(sceneName))
        {
            if (loadingPanel != null)
            {
                loadingPanel.SetActive(true); // Mostrar el Canvas de carga
            }
            StartCoroutine(LoadAsynchronously(sceneName));
        }
        else
        {
            Debug.LogWarning($"{gameObject.name} no tiene asignada ninguna escena.");
        }
    }

    private IEnumerator LoadAsynchronously(string targetScene)
    {
        AsyncOperation operation = SceneManager.LoadSceneAsync(targetScene);
        operation.allowSceneActivation = false;

        while (!operation.isDone)
        {
            float progress = Mathf.Clamp01(operation.progress / 0.9f);

            if (progressBar != null) progressBar.value = progress;
            if (progressText != null) progressText.text = (progress * 100f).ToString("F0") + "%";

            if (operation.progress >= 0.9f)
            {
                operation.allowSceneActivation = true;
            }

            yield return null;
        }
    }
}
