using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionsSpawn : MonoBehaviour {
    public GameObject pociónSaludPrefab; // Asigna el prefab de la poción de salud en el inspector
    public GameObject pociónEnergíaPrefab; // Asigna el prefab de la poción de energía en el inspector
    public float tiempoEntrePociones = 15f; // Tiempo en segundos entre la aparición de pociones
    public int cantidadDeSalud = 5; // Cantidad de salud que restaura la poción de salud
    public int cantidadDeEnergia = 50; // Cantidad de energía que restaura la poción de energía

    void Start() {
        Invoke("GenerarPoción", tiempoEntrePociones);
    }

    void GenerarPoción() {
        TipoPocion tipoDePocion = (TipoPocion)Random.Range(0, 2);
        GameObject pociónPrefab = tipoDePocion == TipoPocion.Salud ? pociónSaludPrefab : pociónEnergíaPrefab;
        GameObject poción = Instantiate(pociónPrefab, new Vector3(-6.487f, -1.647f, 0), Quaternion.identity);

        Pocion componentePocion = poción.GetComponent<Pocion>();
        componentePocion.tipoDePocion = tipoDePocion;
        if (tipoDePocion == TipoPocion.Salud) {
            componentePocion.cantidadDeSalud = cantidadDeSalud;
        } else {
            componentePocion.cantidadDeEnergia = cantidadDeEnergia;
        }
        Invoke("GenerarPoción", tiempoEntrePociones);
    }
}
