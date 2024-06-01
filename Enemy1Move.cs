using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Eney1Move : MonoBehaviour
{
    [SerializeField] int vidaBoss = 20;
    [SerializeField] Slider sliderVidasBoss;
    private Rigidbody2D Rigidbody2D;
    private Animator animator;
    public GameObject fireballPrefab; 
    public GameObject fireballPrefab2; 
    public GameObject YOUWIN;    
    public GameObject otherEnemy;

    private bool isTimeStopped = false;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        StartCoroutine(ShotFireRoutine());
        sliderVidasBoss.maxValue = vidaBoss;
        sliderVidasBoss.value = vidaBoss;
    }

    IEnumerator ShotFireRoutine()
    {
        while (true) // Bucle infinito.
        {
            animator.SetBool("casting", true);
            yield return new WaitForSeconds(2.5f); // Espera 10 segundos.
            // Llama a la función de disparo.
        }
    }

    void Update()
    {
        
    }

    private void ShotFire()
    {
        animator.SetBool("casting", true);
        GameObject fireballPrefabToUse = transform.position.x > 0 ? fireballPrefab : fireballPrefab2;

        float offsetX;
        float offsetY;

        if (fireballPrefabToUse.tag == "Fuego2")
        {
            offsetX = 0.5f;
            offsetY = 0.15f;
        }
        else if (fireballPrefabToUse.tag == "Fuego")
        {
            offsetX = -0.5f;
            offsetY = -0.15f;
        }
        else
        {
            offsetX = 0f;
            offsetY = 0f;
        }

        Instantiate(
            fireballPrefabToUse,
            new Vector3(transform.position.x + offsetX, transform.position.y + offsetY, transform.position.z),
            Quaternion.identity
        );

        animator.SetBool("casting", false);
    }

    public void TomarDaño(int daño)
    {
        vidaBoss -= daño;
        sliderVidasBoss.value = vidaBoss;
        if (vidaBoss <= 0)
        {
            Debug.Log("El enemigo ha muerto.");
            CheckVictoryCondition();
            StartCoroutine(EsperarYDestruir());
        }
    }

    private void CheckVictoryCondition()
    {
        if (otherEnemy == null || otherEnemy.GetComponent<Eney1Move>().vidaBoss <= 0)
        {
            StartCoroutine(EsperarYDetenerTiempo());
        }
    }

    private IEnumerator EsperarYDetenerTiempo()
    {
        yield return new WaitForSeconds(0.6f);
        YOUWIN.SetActive(true);
        Destroy(gameObject); 
        Time.timeScale = 0;
        isTimeStopped = true;        
    }

    private IEnumerator EsperarYDestruir()
    {
        animator.SetBool("morision", true);
        yield return new WaitForSeconds(0.6f);
        Destroy(gameObject); 
        
    }
}
