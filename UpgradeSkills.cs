using UnityEngine;
using UnityEngine.SceneManagement;

public class UpgradeSkills : MonoBehaviour
{
    private PlayerCombat playerCombat;
    private PlayerHealth playerHealth;

    void Start()
    {
        playerCombat = FindObjectOfType<PlayerCombat>();
        playerHealth = FindObjectOfType<PlayerHealth>();
    }

    public void UpgradeHealth()
    {
        if (playerHealth != null)
        {
            playerHealth.UpgradeHealth();
            RestorePlayerStats();
            LoadUpgradeScene();
        }
    }

    public void UpgradePower()
    {
        if (playerCombat != null)
        {
            playerCombat.UpgradePower();
            RestorePlayerStats();
            LoadUpgradeScene();
        }
    }

    public void UpgradeEnergy()
    {
        if (playerCombat != null)
        {
            playerCombat.UpgradeEnergy();
            RestorePlayerStats();
            LoadUpgradeScene();
        }
    }

    private void RestorePlayerStats()
    {
        if (playerHealth != null)
        {
            playerHealth.RestaurarSalud(playerHealth.GetVidaBase()); // Restaura la salud al máximo
        }
        if (playerCombat != null)
        {
            playerCombat.RestaurarEnergia(playerCombat.GetBaseEnergy()); // Restaura la energía al máximo
        }
    }

    void LoadUpgradeScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        Time.timeScale = 1;
    }
}
