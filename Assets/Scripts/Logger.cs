using UnityEngine;

// Script para la tarea "5. Validaciones y Optimización"
public static class Logger
{
    // Método para log de éxito (información positiva)
    public static void LogSuccess(string message)
    {
        Debug.Log($"<color=green> Success: {message} </color>");
    }

    // Método para log de advertencia
    public static void LogWarning(string message)
    {
        Debug.LogWarning($"<color=yellow> Warning: {message} </color>");
    }

    // Método para log de error
    public static void LogError(string message)
    {
        Debug.LogError($"<color=red> Error: {message} </color>");
    }

    // Método para log estándar (información general)
    public static void Log(string message)
    {
        Debug.Log($"<color=white> {message} </color>");
    }
}
