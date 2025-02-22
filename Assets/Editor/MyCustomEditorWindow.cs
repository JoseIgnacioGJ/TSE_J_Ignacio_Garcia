using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;
using System.IO;
using System.Linq;

// Script para la tarea "3. Crear una Ventana Personalizada en el Editor"
public class MyCustomEditorWindow : EditorWindow
{
    Vector2 scrollPos; // Posición del scroll
    Object draggedScene; // Escena arrastrada

    // Crear un acceso al menú
    [MenuItem("Tools/Construir Escenas")]
    public static void ShowWindow()
    {
        // Nombre de la pestaña en la ventana
        GetWindow<MyCustomEditorWindow>("Construir Escenas"); 
    }

    // Método para definir los elementos de la interfaz
    private void OnGUI()
    {
        // Texto informativo
        GUILayout.Label("Escenas en Build Settings", EditorStyles.boldLabel);

        // Botón para actualizar la lista
        if (GUILayout.Button("Actualizar Lista"))
        {
            Repaint();
        }

        // Espacio entre dos componentes
        GUILayout.Space(10);

        // ==========================
        // Área de Drag & Drop
        // ==========================
        GUILayout.Label("Arrastra aquí una escena para agregarla al Build Settings", EditorStyles.helpBox);
        Rect dropArea = GUILayoutUtility.GetRect(0, 50, GUILayout.ExpandWidth(true));
        GUI.Box(dropArea, "Arrastra la escen aquí", EditorStyles.centeredGreyMiniLabel);

        // Comprobar eventos de Drag & Drop
        Event evt = Event.current;
        if (evt.type == EventType.DragUpdated || evt.type == EventType.DragPerform)
        {
            if (dropArea.Contains(evt.mousePosition))
            {
                DragAndDrop.visualMode = DragAndDropVisualMode.Copy;

                if (evt.type == EventType.DragPerform)
                {
                    DragAndDrop.AcceptDrag();
                    foreach (Object obj in DragAndDrop.objectReferences)
                    {
                        // Se comprueba si es un Asset de una escena lo que se está arrastrando
                        if (obj is SceneAsset)
                        {
                            draggedScene = obj; // Guardamos el objeto arrastrado en la variable
                        }
                    }
                }
                evt.Use();
            }
        }

        // Se comprueba si se ha arrastrado una escena válida
        if (draggedScene != null)
        {
            GUILayout.Space(10);
            GUILayout.Label("Escena seleccionada: " + draggedScene.name, EditorStyles.boldLabel);

            if (GUILayout.Button("Agregar al Build Settings"))
            {
                AddSceneToBuildSettings(draggedScene);
                draggedScene = null;
                Repaint();
            }

            if (GUILayout.Button("Cancelar"))
            {
                draggedScene = null;
            }
        }

        GUILayout.Space(20);

        // =================================
        // Listado de escenas en Build
        // =================================
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true); // Permite hacer scroll si hay muchas escenas

        EditorBuildSettingsScene[] buildScenes = EditorBuildSettings.scenes; // Recupera todas las escenas del Build Settings
        string activeScenePath = EditorSceneManager.GetActiveScene().path;

        if (buildScenes.Length == 0)
        {
            GUILayout.Label("No hay escenas en el Build Settings.");
        }
        else
        {
            foreach (EditorBuildSettingsScene scene in buildScenes)
            {
                // Verifica si la escena está marcada como activa para el build
                if (scene.enabled)
                {
                    string scenePath = scene.path;
                    string sceneName = Path.GetFileNameWithoutExtension(scenePath);

                    GUILayout.BeginHorizontal();

                    // Marcar la escena activa
                    if (scenePath == activeScenePath)
                    {
                        GUILayout.Label(sceneName, EditorStyles.boldLabel);
                    }
                    else
                    {
                        GUILayout.Label(sceneName);
                    }

                   

                    // Botón para recargar si es la escena activa
                    if (scenePath == activeScenePath)
                    {
                        if (GUILayout.Button("Recargar", GUILayout.Width(70)))
                        {
                            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                            {
                                EditorSceneManager.OpenScene(activeScenePath); // Abre la escena seleccionada
                            }
                        }
                    }
                    else
                    {
                        // Botón para abrir una escena cerrada
                        if (GUILayout.Button("Abrir", GUILayout.Width(60)))
                        {
                            if (EditorSceneManager.SaveCurrentModifiedScenesIfUserWantsTo())
                            {
                                EditorSceneManager.OpenScene(scenePath);
                            }
                        }
                    }

                    GUILayout.EndHorizontal();
                }
            }
        }

        GUILayout.EndScrollView();
    }

    // Método para agregar una escena
    void AddSceneToBuildSettings(Object sceneAsset)
    {
        string scenePath = AssetDatabase.GetAssetPath(sceneAsset);

        // Verificar si la escena ya está en el Build Settings
        EditorBuildSettingsScene[] currentScenes = EditorBuildSettings.scenes;
        bool alreadyAdded = currentScenes.Any(s => s.path == scenePath);

        if (alreadyAdded)
        {
            // Muestra una ventana emergente
            EditorUtility.DisplayDialog("Información", "La escena ya está en el Build Settings.", "OK");
            return;
        }

        // Agregar la nueva escena
        var newScene = new EditorBuildSettingsScene(scenePath, true);
        var updatedScenes = currentScenes.Append(newScene).ToArray();
        EditorBuildSettings.scenes = updatedScenes;

        // Muestra una ventana emergente
        EditorUtility.DisplayDialog("Escena Agregada", $"La escena '{sceneAsset.name}' fue añadida al Build Settings.", "OK");
    }
}
