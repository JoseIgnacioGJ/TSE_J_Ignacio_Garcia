using System.Collections;
using UnityEngine;
using UnityEngine.Android;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

// Script para la tarea "2. Solicitar Permisos de Ubicación"
public class LocationPermission : MonoBehaviour
{
    private Text textMesh; // Texto que informa al usuario
    private Button button; // Botón con el logo de la ubicación
    private Image image; // Fondo de pantalla que cambia de color según si la ubicación está o no activada

    void Start()
    {
        // Se recogen los componentes visibles de la UI
        image = this.GetComponent<Image>();
        textMesh = this.GetComponentInChildren<Text>();
        button = this.GetComponentInChildren<Button>();

        // Se verifica los permisos en Android
        if (Application.platform == RuntimePlatform.Android)
        {
            if (!Permission.HasUserAuthorizedPermission(Permission.FineLocation))
            {
                Permission.RequestUserPermission(Permission.FineLocation);
            }
        }
    }

    void Update()
    {
        // Se comprueba si el usuario ha activado o no la ubicación y le informamos de la situación con el mensaje y el color de fondo
        if (Input.location.isEnabledByUser)
        {
            button.gameObject.SetActive(false);
            image.color = Color.green;
            textMesh.text = "Permisos de ubicación otorgados. Bienvenido.";
        }
        else
        {
            button.gameObject.SetActive(true);
            image.color = Color.red;
            textMesh.text = "Permisos de ubicación rechazados. Por favor, pulsa el logo rojo y activa tu ubicación.";
        }
    }

    // Método para abrir la configuración del dispositivo usando un Intent en Android
    public void OpenLocationSettings()
    {
        #if UNITY_ANDROID
            using (AndroidJavaClass unityPlayer = new AndroidJavaClass("com.unity3d.player.UnityPlayer"))
            {
                AndroidJavaObject currentActivity = unityPlayer.GetStatic<AndroidJavaObject>("currentActivity");
                AndroidJavaObject intent = new AndroidJavaObject("android.content.Intent", "android.settings.LOCATION_SOURCE_SETTINGS");
                currentActivity.Call("startActivity", intent);
            }
        #endif
    }
}
