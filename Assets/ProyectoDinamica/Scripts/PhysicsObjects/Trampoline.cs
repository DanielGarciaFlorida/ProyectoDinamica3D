using UnityEngine;

public class Trampoline : MonoBehaviour
{
    public float jumpForce = 10f;

    
    
    public float dragOnJump = 0.5f;


    private void OnTriggerEnter(Collider other)
    {
        //Comprobamos si lo que entró es la bola
        if (other.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                //Aplicamos una fuerza instantánea hacia arriba
                ballRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                //Aplicamos amortiguación
                ballRigidbody.linearDamping = dragOnJump;

            }
        }
    }

}