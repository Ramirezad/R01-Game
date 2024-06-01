using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject canvasOptions;
    public GameObject storyCanva;

    // Esta función se llama en cada frame
    void Update()
    {
        // Comprueba si storyCanva está activo en la escena
        if (storyCanva.activeInHierarchy)
        {
            if (Input.GetKeyDown(KeyCode.Return))
            {
                // Si se presiona Enter, carga la escena con el índice especificado
                SceneManager.LoadScene(1);
            }
            else if (Input.GetKeyDown(KeyCode.Escape))
            {
                // Si se presiona Escape, desactiva storyCanva
                storyCanva.SetActive(false);
            }
        }
    }

    public void CambiarNivel()
    {
        // Activa storyCanva
        storyCanva.SetActive(true);
    }

    public void OpenOptions()
    {
        canvasOptions.SetActive(!canvasOptions.activeSelf);
    }

    public void CloseOptions()
    {
        canvasOptions.SetActive(false);
    }

    public void ExitGame()
    {
        Application.Quit();
    }
}

