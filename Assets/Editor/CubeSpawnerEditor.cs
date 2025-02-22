using UnityEngine;
using UnityEditor;

// Script para la tarea "4. Generar un Cubo en la Escena mediante un Botón en el Editor" y
// "6. Tareas Adicionales (Opcionales, pero recomendadas)"
[CustomEditor(typeof(CubeSpawner))]
public class CubeSpawnerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        // Dibujar el inspector predeterminado
        DrawDefaultInspector();

        CubeSpawner spawner = (CubeSpawner)target;

        GUILayout.Space(10); // Espacio entre los componentes

        // Botón para generar cubos
        if (GUILayout.Button($"Spawn {spawner.cubesPerSpawn} Cubo(s)"))
        {
            spawner.SpawnCube();
        }

        GUILayout.Space(10); // Espacio entre los componentes

        // Botón para eliminar todos los cubos
        if (GUILayout.Button("Eliminar Todos los Cubos"))
        {
            spawner.RemoveAllCubes();
        }
    }
}
