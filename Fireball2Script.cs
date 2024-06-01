using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fireball2 : MonoBehaviour
{
    public float Speed;
    private Rigidbody2D Rigidbody2D;

    void Start()
    {
        Rigidbody2D = GetComponent<Rigidbody2D>();
    }

    private void FixedUpdate()
    {
        // Cambiar la dirección de movimiento a la derecha
        Rigidbody2D.velocity = Vector2.right * Speed;
    }

    void OnCollisionEnter2D(Collision2D col)
    {
        if(col.gameObject.CompareTag("Player"))
        {
            // Obtener la normal del punto de contacto para determinar la dirección del rebote
            ContactPoint2D contact = col.GetContact(0);
            Vector2 knockbackDirection = contact.normal;

            // Llamar a TakeDamage con el daño y la dirección del rebote
            col.gameObject.GetComponent<PlayerHealth>().TakeDamage(2, knockbackDirection);
        }

        Destroy(gameObject);
    }
}


