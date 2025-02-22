using UnityEngine;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine.UI;

// Script para la tarea "4. Generar un Cubo en la Escena mediante un Botón en el Editor" y
// "6. Tareas Adicionales (Opcionales, pero recomendadas)"
public class CubeSpawner : MonoBehaviour
{
    private Text cubeNumberText; // Texto que informa al usuario la cantidad de cubos que hay en escena
    private int cubeNumber = 0; // Cantidad de cubos que hay en escena
    private Material randomMaterial; // Material que se asigna al cubo que se creará
    private string[] guids; // Vector para guardar los materiales que hay en el proyecto

    // Ruta del proyecto donde se encuentra la carpeta que contiene los materirales
    [Header("Carpeta con Materiales")]
    public string folderPath = "Assets/Materials";

    // El prefab del cubo que se usa para generar los cubos en escenas
    [Header("Prefab del Cubo")]
    public GameObject cubePrefab;

    // Los vectores que determinan el espacio donde se encontrarán los cubos
    [Header("Rango de Posiciones")]
    public Vector3 minRange = new Vector3(-5, 0, -5);
    public Vector3 maxRange = new Vector3(5, 5, 5);

    // Número de cubos por pulsación
    [Header("Opciones de Generación")]
    [Range(1, 100)]
    public int cubesPerSpawn = 1;

    private List<GameObject> cubePool = new List<GameObject>(); // Pool de cubos inactivos
    private List<GameObject> activeCubes = new List<GameObject>(); // Lista de cubos activos

    // Opción para generar automáticamente una cantidad de cubos cuando se inicie la escena
    [Header("Generar al Iniciar")]
    public bool spawnOnStart = false;

    void Start()
    {
        // Obtenemos el texto informativo
        cubeNumberText = this.GetComponentInChildren<Text>();

        // Se buscan los Assets que sean de tipo "Material" y que se encuentren en la ruta "folderPath"
        guids = AssetDatabase.FindAssets("t:Material", new[] { folderPath });

        // Si la carpeta de la ruta está vacía, informamos por consola y se usará el material por defecto
        if (guids.Length == 0)
        {
            Debug.LogWarning($"No se encontraron materiales en la carpeta: {folderPath}");
        }

        // Si el checkbox está activado, se generan los cubos automáticamente
        if (spawnOnStart)
        {
            SpawnCube();
        }
    }

    // Método para obtener un cubo del pool
    private GameObject GetCubeFromPool()
    {
        foreach (GameObject cube in cubePool)
        {
            if (!cube.activeInHierarchy) // Si el cubo está inactivo, lo reutilizamos
            {
                cube.SetActive(true); // Activamos el cubo
                return cube;
            }
        }

        // Si no hay cubos disponibles en el pool, instanciamos un nuevo cubo
        GameObject newCube = Instantiate(cubePrefab);

        // Vemos si hay varios materiales disponibles
        if (guids.Length != 0)
        {
            string randomGuid = guids[Random.Range(0, guids.Length)]; // Una vez que tenemos los GUIDs, seleccionamos uno aleatoriamente
            string path = AssetDatabase.GUIDToAssetPath(randomGuid); // Convertimos ese GUID en una ruta

            
            randomMaterial = AssetDatabase.LoadAssetAtPath<Material>(path); // Cargamos el material desde el path
            newCube.GetComponent<Renderer>().material = randomMaterial; // Cambiamos el material de la instancia
        }
            

        cubePool.Add(newCube); // Agregamos el nuevo cubo al pool
        return newCube;
    }

    // Método para instanciar cubos
    public void SpawnCube()
    {
        // Colocamos aleatoriamente los cubos que el usuario quiere generar, teniendo
        // en cuenta la cantidad de éstos y los límites de espacio establecidos
        for (int i = 0; i < cubesPerSpawn; i++)
        {
            Vector3 randomPosition = new Vector3(
                Random.Range(minRange.x, maxRange.x),
                Random.Range(minRange.y, maxRange.y),
                Random.Range(minRange.z, maxRange.z)
            );

            GameObject cube = GetCubeFromPool(); // Obtenemos un cubo del pool
            cube.transform.position = randomPosition; // Establecemos su posición
            activeCubes.Add(cube); // Lo agregamos a la lista de cubos activos
        }

        Debug.Log($"Generados {cubesPerSpawn} cubos.");
        cubeNumber += cubesPerSpawn; // Incrementamos el contador de los cubos que se muestran por pantalla
    }

    // Método para desactivar todos los cubos generados
    public void RemoveAllCubes()
    {
        foreach (GameObject cube in activeCubes)
        {
            cube.SetActive(false); // Desactivamos cada cubo
        }

        activeCubes.Clear(); // Limpiamos la lista de cubos activos
        Debug.Log("Todos los cubos han sido eliminados.");
        cubeNumber = 0; // Ponemos a 0 el contador
    }

    private void Update()
    {
        // Actualizamos en todo momento el texto informativo del UI
        cubeNumberText.text = "Total de cubos: " + cubeNumber;
    }
}
