using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerHealth : MonoBehaviour
{
    public static int baseVidasIniciales = 10;
    public static int vidasIniciales = baseVidasIniciales;
    public int vidas;
    [SerializeField] Slider sliderVidas;
    public GameObject YOULOSE;

    private bool hurt;
    private PlayerMove playerMove;
    [SerializeField] private float tiempoPerdidaControl;
    private Animator animator;
    private PlayerCombat playerCombat;

    public GameObject UpgradeSkills;

    public GameObject YOUWIN;  

    void Start()
    {
        // Cargar las vidas guardadas o usar las vidas iniciales configuradas
        vidasIniciales = PlayerPrefs.GetInt("vidasIniciales", vidasIniciales);
        vidas = PlayerPrefs.GetInt("vidas", vidasIniciales);

        if (sliderVidas != null)
        {
            sliderVidas.maxValue = vidas;
            sliderVidas.value = vidas;
        }
        playerMove = GetComponent<PlayerMove>();
        animator = GetComponent<Animator>();
        playerCombat = FindObjectOfType<PlayerCombat>();
    }

    void Update()
    {
        if (animator != null)
        {
            animator.SetBool("Golpe", hurt);
        }
        hurt = false;

        if (YOUWIN.activeSelf && Input.GetKeyDown(KeyCode.Return) && SceneManager.GetActiveScene().buildIndex != 2)
        {
            YOUWIN.SetActive(false);
            UpgradeSkills.SetActive(true);                   
        } else if (YOUWIN.activeSelf && Input.GetKeyDown(KeyCode.Return)){
            SceneManager.LoadScene(0);
        }

        if (YOULOSE.activeSelf && Input.GetKeyDown(KeyCode.Return))
        {
            SceneManager.LoadScene(0);
        }
    }

    public void SetVidas(int nuevasVidas)
    {
        vidas = nuevasVidas;
        if (sliderVidas != null)
        {
            sliderVidas.maxValue = vidas;
            sliderVidas.value = vidas;
        }
    }

    public void TakeDamage(int damage, Vector2 puntoGolpe)
    {
        vidas -= damage;
        if (sliderVidas != null)
        {
            sliderVidas.value = vidas;
        }
        animator.SetTrigger("Golpe");
        StartCoroutine(PerderControl());
        playerMove.Rebote(puntoGolpe);
        hurt = true;

        if (vidas <= 0)
        {
            Debug.Log("El jugador ha muerto.");
            Time.timeScale = 0;
            YOULOSE.SetActive(true);
        }
    }

    private IEnumerator PerderControl()
    {
        playerMove.sePuedeMover = false;
        yield return new WaitForSeconds(tiempoPerdidaControl);
        playerMove.sePuedeMover = true;
    }

    public void RestaurarSalud(int saludExtra)
    {
        vidas = Mathf.Min(vidas + saludExtra, vidasIniciales); // Incrementa vidas sin exceder el máximo
        if (sliderVidas != null)
        {
            sliderVidas.value = vidas;
        }
    }

    public void UpgradeHealth()
    {
        vidas = vidasIniciales+5;
        baseVidasIniciales = vidas;

        //úrate de que el jugador obtenga la nueva cantidad de vidas
        if (sliderVidas != null)
        {
            sliderVidas.maxValue = vidas;
            sliderVidas.value = vidas;
        }
        Debug.Log($"Health upgraded to: {vidas}");

        // Guardar las nuevas vidas y vidas iniciales en PlayerPrefs
        PlayerPrefs.SetInt("vidasIniciales", vidasIniciales);
        PlayerPrefs.SetInt("vidas", vidas);
        PlayerPrefs.Save();
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("Pocion"))
        {
            Pocion pocion = other.GetComponent<Pocion>();

            if (pocion != null)
            {
                if (pocion.tipoDePocion == TipoPocion.Salud)
                {
                    Debug.Log("Estás colisionando con una poción de salud.");
                    RestaurarSalud(pocion.cantidadDeSalud);
                }
                else if (pocion.tipoDePocion == TipoPocion.Energia)
                {
                    Debug.Log("Estás colisionando con una poción de energía.");
                    RestaurarEnergia(pocion.cantidadDeEnergia);
                }

                Destroy(other.gameObject);
            }
            else
            {
                Debug.LogError("No se encontró el componente Pocion en el objeto con el que se colisionó.");
            }
        }
    }

    private void OnCollisionEnter2D(Collision2D otro)
    {
        if (otro.gameObject.CompareTag("Fuego"))
        {
            Destroy(otro.gameObject);
        }
        else if (otro.gameObject.CompareTag("MapaRechazo"))
        {
            ContactPoint2D contact = otro.GetContact(0);
            Vector2 knockbackDirection = contact.normal;
            StartCoroutine(PerderControl());
            playerMove.Rebote(knockbackDirection);
        }
    }

    public void RestaurarEnergia(int energiaExtra)
    {
        
        playerCombat.AddEnergy(energiaExtra);
    }

    public int GetVidaBase()
    {
        return baseVidasIniciales;
    }
}
