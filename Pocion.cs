using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pocion : MonoBehaviour
{
    public TipoPocion tipoDePocion;
    public int cantidadDeSalud;
    public int cantidadDeEnergia;
}

public enum TipoPocion
{
    Salud,
    Energia
}
