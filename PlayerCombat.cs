    using System.Collections;
    using System.Collections.Generic;
    using UnityEngine;
    using UnityEngine.UI;

public class PlayerCombat : MonoBehaviour
{
    public static PlayerCombat instance;

    [SerializeField] private Transform controladorGolpe;
    [SerializeField] private float radioGolpe;

    private static int baseEnergy = 350;
    private static int energy;
    [SerializeField] private Slider sliderEnergy; // Ahora es privado

    private static int baseSkillDamageBonus = 0;
    public int skillDamageBonus = 0;

    private bool playerSkill1;
    private bool playerSkill2;
    private bool playerSkill3;
    private bool enRecarga = false;



    private Animator animator;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
            if (energy == 0)
            {
                energy = baseEnergy; // Asegura que la energía inicial sea 350
            }
            skillDamageBonus = baseSkillDamageBonus; // Establece skillDamageBonus al valor base
        }
        else if (instance != this)
        {
            Destroy(gameObject);
        }
    }

    public void UpgradePower()
    {
        baseSkillDamageBonus += 5; // Incrementa el valor base del daño de habilidad
        skillDamageBonus = baseSkillDamageBonus; // Actualiza skillDamageBonus al valor base
    }

    void Start()
    {
        animator = GetComponent<Animator>();
        UpdateEnergySlider();
        StartCoroutine(RecuperarEnergia());
    }

    void Update()
    {
        playerSkill1 = Input.GetKeyDown(KeyCode.H);
        playerSkill2 = Input.GetKeyDown(KeyCode.J);
        playerSkill3 = Input.GetKeyDown(KeyCode.K);

        if (!enRecarga)
        {
            if (playerSkill1 && energy >= 15)
            {
                Golpe(15, (10 + skillDamageBonus));
                animator.SetBool("playerskill1", true);
                StartCoroutine(RecargaHabilidad(0.35f));
                Debug.Log("Energia restante: " + energy);
                Debug.Log("Ahora pegas: " + skillDamageBonus);
            }
            else if (playerSkill2 && energy >= 55)
            {
                Golpe(55, (25 + skillDamageBonus));
                animator.SetBool("playerskill2", true);
                StartCoroutine(RecargaHabilidad(0.5f));
            }
            else if (playerSkill3 && energy >= 30)
            {
                Golpe(30, (15 + skillDamageBonus));
                animator.SetBool("playerskill3", true);
                StartCoroutine(RecargaHabilidad(0.35f));
            }
        }
        else
        {
            animator.SetBool("playerskill1", false);
            animator.SetBool("playerskill2", false);
            animator.SetBool("playerskill3", false);
        }
    }

    IEnumerator RecargaHabilidad(float tiempoRecarga)
    {
        enRecarga = true;
        yield return new WaitForSeconds(tiempoRecarga);
        enRecarga = false;
    }

    IEnumerator RecuperarEnergia()
    {
        while (true)
        {
            yield return new WaitForSeconds(5);
            if (energy + 60 <= sliderEnergy.maxValue)
            {
                AddEnergy(60);
            }
            else
            {
                energy = (int)sliderEnergy.maxValue;
                UpdateEnergySlider();
            }
        }
    }

    private void Golpe(int energySpell, int damageSpell)
    {
        Collider2D[] objetos = Physics2D.OverlapCircleAll(controladorGolpe.position, radioGolpe);

        foreach (Collider2D colisionador in objetos)
        {
            if (colisionador.CompareTag("Enemy"))
            {
                colisionador.transform.GetComponent<Eney1Move>().TomarDaño(damageSpell);
            }
        }

        UseEnergy(energySpell);
    }

    public void AddEnergy(int amount)
    {
        energy = Mathf.Min(energy + amount, (int)sliderEnergy.maxValue);
        UpdateEnergySlider();
    }

    public void UseEnergy(int amount)
    {
        energy -= amount;
        UpdateEnergySlider();
    }

    public void UpgradeEnergy()
    {
        baseEnergy += 100;  // Suma la energía a la energía base
        energy = baseEnergy;  // Actualiza la energía actual a la energía base
        UpdateEnergySlider();
    }

    public int GetBaseEnergy()
    {
        return baseEnergy;
    }

    public void RestaurarEnergia(int energiaExtra)
    {
        energy = energiaExtra; // Establecer energía al máximo permitido
        if (sliderEnergy != null)
        {
            sliderEnergy.value = energy;
        }
    }

    private void UpdateEnergySlider()
    {
        if (sliderEnergy != null)
        {
            sliderEnergy.maxValue = baseEnergy;  // Actualiza el valor máximo del slider a la energía base
            sliderEnergy.value = energy;
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(controladorGolpe.position, radioGolpe);
    }
}
