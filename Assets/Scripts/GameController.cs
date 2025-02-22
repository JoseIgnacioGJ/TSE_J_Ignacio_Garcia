using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script para la tarea "5. Validaciones y Optimización"
public class GameController : MonoBehaviour
{
    [Header("Caso de éxito")]
    public bool success = true;

    void Start()
    {
        Logger.Log("El juego ha comenzado");

        // Un caso de éxito
        if (success)
        {
            Logger.LogSuccess("¡La carga de datos fue exitosa!");
        }
        else
        {
            Logger.LogError("Hubo un problema al cargar los datos.");
        }

        // Simulamos una advertencia
        Logger.LogWarning("El rendimiento podría disminuir si se siguen generando más objetos.");
    }
}
