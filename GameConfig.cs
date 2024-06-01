using UnityEngine;
using TMPro;
using System.Collections.Generic;

public class GameConfig : MonoBehaviour
{
    public static GameConfig Instance;

    public enum Dificultad { Normal, Easy, Hard }
    public Dificultad dificultadSeleccionada;

    public TMP_Dropdown difficultyDropdown;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    void Start()
    {
        if (difficultyDropdown != null)
        {
            InitializeDifficultyDropdown(difficultyDropdown);
            difficultyDropdown.onValueChanged.AddListener(delegate { SetDificultad(difficultyDropdown.value); });
        }
        else
        {
            Debug.LogError("No se ha asignado el TMP_Dropdown de dificultad.");
        }

        SetInitialLives(); // Asegúrate de establecer las vidas iniciales al inicio del juego.
    }

    public void InitializeDifficultyDropdown(TMP_Dropdown dropdown)
    {
        dropdown.ClearOptions();
        dropdown.AddOptions(new List<string> { "Normal", "Easy", "Hard" });
        dropdown.value = (int)dificultadSeleccionada;
        dropdown.RefreshShownValue();
    }

    public void SetDificultad(int dificultadIndex)
    {
        dificultadSeleccionada = (Dificultad)dificultadIndex;
        SetInitialLives();
        Debug.Log($"Dificultad seleccionada: {dificultadSeleccionada}");
    }

    private void SetInitialLives()
    {
        switch (dificultadSeleccionada)
        {
            case Dificultad.Easy:
                PlayerHealth.vidasIniciales = 15;
                break;
            case Dificultad.Normal:
                PlayerHealth.vidasIniciales = 10;
                break;
            case Dificultad.Hard:
                PlayerHealth.vidasIniciales = 5;
                break;
        }

        PlayerPrefs.SetInt("vidasIniciales", PlayerHealth.vidasIniciales);
        PlayerPrefs.SetInt("vidas", PlayerHealth.vidasIniciales);
        PlayerPrefs.Save();
    }
}
