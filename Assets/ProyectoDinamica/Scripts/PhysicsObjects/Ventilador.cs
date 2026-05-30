using UnityEngine;

public class Ventilador : MonoBehaviour
{
    [Header("Configuración del Viento")]
    [Tooltip("Fuerza con la que el viento empujará la bola.")]
    public float windStrength = 5f;

    [Tooltip("Dirección del viento. Por defecto es hacia adelante (Foward) del objeto.")]
    public Vector3 windDirection = Vector3.forward;

    // Usamos FixedUpdate para aplicar fuerzas físicas de manera constante
    private void OnTriggerStay(Collider other)
    {
        // Verificamos si lo que entró en el viento es la bola de golf
        // (Asegúrate de que tu bola tenga la etiqueta "Player" o el tag que prefieras)
        if (other.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Transformamos la dirección local del molino a dirección global
                Vector3 globalWindDirection = transform.TransformDirection(windDirection);

                // Aplicamos la fuerza de manera continua mientras esté dentro del Trigger
                ballRigidbody.AddForce(globalWindDirection * windStrength, ForceMode.Force);
            }
        }
    }

    // Opcional: Dibuja una línea en el editor para ver hacia dónde sopla el viento
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 globalDir = transform.TransformDirection(windDirection);
        Gizmos.DrawRay(transform.position, globalDir * 3f);
    }
}

