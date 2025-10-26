# TSE_J_Ignacio_Garcia
Una serie de ejercicios enfocados en el desarrollo XR con Unity.


===========================================================================
1. Configuración del Proyecto 
2. Solicitar Permisos de Ubicación

- Escena/s:
Assets/Scenes/PermisosUbicación.unity

- Script/s:
Assets/Scripts/LocationPermission.cs

- Archivo/s para instalar el programa:
Example.apk

- Instrucciones: conectar un dispositivo Android al ordenador, introducir el archivo "Example.apk" en el dispositivo y luego en éste, pulsar en el icono del archivo para iniciar la instalación. Una vez instalada la app, se debe entrar en ella y seguir las indicaciones que aparecen por pantalla.

- Nota aparte: si se desea hacer pruebas dentro de la escena de Unity, es necesario ajustar el tamaño de la ventana a 1080x1400 px.

===========================================================================
3. Crear una Ventana Personalizada en el Editor

- Escena/s:
Assets/Scenes/PermisosUbicación.unity
Assets/Scenes/GeneraCubos.unity

- Script/s:
Assets/Editor/MyCustomEditorWindow.cs

- Instrucciones: en el Editor de Unity, en el menú superior, hay que hacer click en "Tools" y luego en "Construir Escenas". Luego, habrá una ventana emergente donde se podrá realizar las tareas pedidas con las escenas del proyecto.

===========================================================================
5. Validaciones y Optimización

- Escena/s:
Assets/Scenes/GeneraCubos.unity

- Script/s:
Assets/Scripts/Logger.cs
Assets/Scripts/GameController.cs

- Instrucciones: dentro de Unity, en la ventana "Hierarchy" de la escena "GeneraCubos", se debe hacer "click" en "Canvas" y luego, en la ventana "Inspector" se podrá ver el script "Game Controller". En éste se puede manejar las opciones del script para luego ejecutarlas en la escena. Al darle al "Play", en la ventana "Console" aparecerán varios mensajes de diferentes colores.

===========================================================================
4. Generar un Cubo en la Escena mediante un Botón en el Editor
6. Tareas Adicionales (Opcionales, pero recomendadas)

- Escena/s:
Assets/Scenes/GeneraCubos.unity

- Script/s:
Assets/Scripts/CubeSpawner.cs
Assets/Editor/CubeSpawnerEditor.cs

- Instrucciones: dentro de Unity, en la ventana "Hierarchy" de la escena "GeneraCubos", se debe hacer "click" en "Canvas" y luego, en la ventana "Inspector" se podrá ver el script "Cube Spawner". En éste se puede manejar las opciones del script antes y/o durante la ejecución de la escena. Al darle al "Play", se pondrá en marcha la escena y donde se puede ver lo que ocurre en ésta mediante la ventana "Console". Es necesario que el tamaño de la ventana de la escena esté en "Free Aspect".

- Nota aparte: se ha optimizado la generación de cubos para evitar acumulaciones excesivas de objetos en la jerarquía. Para ello, en el código se ha seguido la siguiente estrategia: en lugar de crear un cubo cada vez que se solicita, se reutiliza los cubos ya generados que no están activos. Y en lugar de destruir los cubos, se desactivan cuando no sean necesarios. Esto ayuda a mantener la jerarquía más limpia. Además, evita la creación y destrucción constantes de GameObjects, lo cual es costoso en términos de rendimiento.

===========================================================================
