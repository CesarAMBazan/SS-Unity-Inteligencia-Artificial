using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class GraphicsListener : MonoBehaviour
{
    // Atributos de la clase PauseManu
    public GameObject graphicsDropdownObject;
    public GameObject fpsDropdownObject;

    // Atributos privados

    private Dictionary<string, int> _qualityLevels;
    private TMP_Dropdown _graphicsDropdown;
    private TMP_Dropdown _fpsDropdown;

    /// <summary>
    /// El método Start corre automaticamente al crear el GameObject al que este componente esta enlazado.
    /// </summary>
    private void Start()
    {
        // Inicializa el diccionario con los niveles de calidad gráfica
        _qualityLevels = new Dictionary<string, int>
        {
            { "Muy Bajos", 0 },
            { "Bajos", 1 },
            { "Medios", 2 },
            { "Altos", 3 },
            { "Muy Altos", 4 },
            { "Ultra", 5 }
        };

        // Obtén los componentes TMP_Dropdown de los GameObjects asignados
        _graphicsDropdown = graphicsDropdownObject.GetComponent<TMP_Dropdown>();
        _fpsDropdown = fpsDropdownObject.GetComponent<TMP_Dropdown>();

        // Establece las opciones del TMP_Dropdown de calidad gráfica
        _graphicsDropdown.options.Clear();
        _graphicsDropdown.AddOptions(new List<string>(_qualityLevels.Keys));

        // Establece las opciones del TMP_Dropdown de FPS
        _fpsDropdown.options.Clear();
        _fpsDropdown.AddOptions(new List<string> { "24 FPS", "30 FPS", "60 FPS" });

        // Obtén la configuración actual de calidad gráfica y FPS
        int currentQualityLevel = QualitySettings.GetQualityLevel();
        int currentFPS = Application.targetFrameRate;

        // Establece el TMP_Dropdown de calidad gráfica en la opción correspondiente a la configuración actual
        _graphicsDropdown.SetValueWithoutNotify(currentQualityLevel);

        // Establece el TMP_Dropdown de FPS en la opción correspondiente a la configuración actual
        int fpsIndex = GetFPSIndex(currentFPS);
        _fpsDropdown.SetValueWithoutNotify(fpsIndex);

        // Suscribir la función OnGraphicsQualityChanged al evento onValueChanged del TMP_Dropdown de calidad gráfica
        _graphicsDropdown.onValueChanged.AddListener(OnGraphicsQualityChanged);
        // Suscribir la función OnGraphicsQualityChanged al evento onValueChanged del TMP_Dropdown de calidad gráfica
        _fpsDropdown.onValueChanged.AddListener(OnFPSLimitChanged);
    }

    // Este método busca y devuelve el índice correspondiente al nivel de calidad gráfica en el Dropdown
    private int GetQualityIndex(string qualityName)
    {
        foreach (var kvp in _qualityLevels)
        {
            if (kvp.Key == qualityName)
            {
                return kvp.Value;
            }
        }

        return 0; // Si no se encuentra, devuelve el índice 0 (Muy Bajos) por defecto
    }

    // Este método busca y devuelve el índice correspondiente al límite de FPS en el Dropdown
    private int GetFPSIndex(int targetFPS)
    {
        for (int i = 0; i < _fpsDropdown.options.Count; i++)
        {
            string optionText = _fpsDropdown.options[i].text;

            if (optionText == targetFPS + " FPS")
            {
                return i;
            }
        }

        return 3; // Si no se encuentra, devuelve el índice 3 (60 FPS) por defecto
    }

    // Este método se asignará al evento OnValueChanged del Dropdown de calidad gráfica
    public void OnGraphicsQualityChanged(int qualityIndex)
    {
        Debug.Log("Calidad grafica cambiada a: " + qualityIndex);
        // Obtén la opción seleccionada en el Dropdown de calidad gráfica
        string selectedGraphicsOption = _graphicsDropdown.options[qualityIndex].text;

        // Configura la calidad gráfica según la opción seleccionada
        if (_qualityLevels.TryGetValue(selectedGraphicsOption, out var level))
        {
            QualitySettings.SetQualityLevel(level);
        }
    }


    /// Este método se asignará al evento OnValueChanged del Dropdown de FPS
    public void OnFPSLimitChanged(int fpsIndex)
    {
        Debug.Log("Fps Index cambiado a  " + fpsIndex);
        // Obtén la opción seleccionada en el Dropdown de FPS
        string selectedFPSOption = _fpsDropdown.options[fpsIndex].text;

        // Configura el límite de FPS según la opción seleccionada
        switch (selectedFPSOption)
        {
            case "24 FPS":
                Application.targetFrameRate = 24;
                break;
            case "30 FPS":
                Application.targetFrameRate = 30;
                break;
            case "60 FPS":
                Application.targetFrameRate = 60;
                break;
            default:
                Application.targetFrameRate = -1; // Sin límite
                break;
        }
    }
}