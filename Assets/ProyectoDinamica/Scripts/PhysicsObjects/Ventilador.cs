using UnityEngine;

public class Ventilador : MonoBehaviour
{
   public float windStrength = 5f;

   public Vector3 windDirection = Vector3.forward;

    
    private void OnTriggerStay(Collider other)
    {
        // Verificamos si lo que entró en el viento es la bola de gol       
        if (other.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Transformamos la dirección local del molino a dirección global
                Vector3 globalWindDirection = transform.TransformDirection(windDirection);

                // Aplicamos la fuerza de manera continua mientras esté dentro del trigger
                ballRigidbody.AddForce(globalWindDirection * windStrength, ForceMode.Force);
            }
        }
    }

    //Dibuja una línea para indicar la dirección del viento con un gizmo
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.cyan;
        Vector3 globalDir = transform.TransformDirection(windDirection);
        Gizmos.DrawRay(transform.position, globalDir * 3f);
    }
}

