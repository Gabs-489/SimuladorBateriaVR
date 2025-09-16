using UnityEngine;

[RequireComponent(typeof(AudioSource))]
public class BalanceBoardSound : MonoBehaviour
{
    public BalanceBoardSensor boardSensor; // Referencia a tu BalanceBoardSensor
    private AudioSource audioSource;

    [Header("Peso Trigger Settings")]
    public double threshold = 0.1; // Incremento mínimo para disparar sonido

    private double previousWeight = 0f;

    void Start()
    {
        audioSource = GetComponent<AudioSource>();
        if (boardSensor == null)
        {
            Debug.LogError("BalanceBoardSensor no asignado.");
        }
    }

    void Update()
    {
        if (boardSensor == null) return;

        double weightChange = boardSensor.totalWeight - previousWeight;

        if (weightChange > threshold)
        {
            PlaySound();
        }

        previousWeight = boardSensor.totalWeight;
    }

    private void PlaySound()
    {
        audioSource.pitch = Random.Range(0.8f, 1.2f); // Variación de tono
        audioSource.Play();
    }
}
