using UnityEngine;
using System;

public class BalanceBoardSensor : MonoBehaviour
{
    [Header("Sensor Values (Kg)")]
    public double rwTopLeft = 0f;
    public double rwTopRight = 0f;
    public double rwBottomLeft = 0f;
    public double rwBottomRight = 0f;
    
    [Header("Calculated Values")]
    public double totalWeight = 0f;
    public Vector2 centerOfPressure = Vector2.zero;
    
    [Header("Latency Monitoring")]
    public double latency = 0f;
    public double avglatency = 0f;
    public double minlatency = double.MaxValue;
    public double maxlatency = 0f;
    public double avg_ms_per_reading = 0f;
    public double prev_ms = 0f;
    public int latency_count = 0;

    private UDPSocketUnity server = new UDPSocketUnity();
    
    void Start()
    {
        // Iniciar servidor UDP en puerto 27335
        server.Server("127.0.0.1", 27335, this);
        Debug.Log("Balance Board UDP Server iniciado en puerto 27335");
    }
    
    void Update()
    {
        // Calcular peso total
        totalWeight = rwTopLeft + rwTopRight + rwBottomLeft + rwBottomRight;
        
        // Calcular centro de presión (normalizado entre -1 y 1)
        if (totalWeight > 0.1f) // Evitar división por cero con pequeña tolerancia
        {
            float x = ((float)(rwTopRight + rwBottomRight) - (float)(rwTopLeft + rwBottomLeft)) / (float)totalWeight;
            float y = ((float)(rwTopLeft + rwTopRight) - (float)(rwBottomLeft + rwBottomRight)) / (float)totalWeight;
            
            centerOfPressure = new Vector2(x, y);
        }
        else
        {
            centerOfPressure = Vector2.zero;
        }
    }
    
    void OnGUI()
    {
        // Mostrar datos en pantalla para debug
        GUI.Label(new Rect(10, 10, 300, 20), $"Weight: {totalWeight:F2} kg");
        GUI.Label(new Rect(10, 30, 300, 20), $"Center: ({centerOfPressure.x:F2}, {centerOfPressure.y:F2})");
        GUI.Label(new Rect(10, 50, 300, 20), $"TL: {rwTopLeft:F2}    TR: {rwTopRight:F2}");
        GUI.Label(new Rect(10, 70, 300, 20), $"BL: {rwBottomLeft:F2}    BR: {rwBottomRight:F2}");
        GUI.Label(new Rect(10, 90, 300, 20), $"Latency: {latency:F1}ms (avg: {avglatency:F1}ms)");
    }
    
    public void display(string message)
    {
        Debug.Log(message);
    }
    
    public void AddLatency(double lat)
    {
        // Función para agregar latencia a estadísticas
        // Implementación adicional si es necesaria
    }
}