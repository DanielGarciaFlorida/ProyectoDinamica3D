using UnityEngine;

public class Trampoline : MonoBehaviour
{
    [Header("Configuración del Trampolín")]
    [Tooltip("La fuerza inicial que impulsará la bola hacia arriba. Ajusta este valor según la altura que desees.")]
    public float jumpForce = 10f;

    [Tooltip("El factor de amortiguación. Define qué tan rápido se disipa la fuerza ascendente. Valores más altos harán que suba y baje más rápido.")]
    [Range(0f, 1f)]
    public float dragOnJump = 0.5f;

    [Tooltip("El sonido que se reproducirá al saltar (opcional).")]
    public AudioClip jumpSound;

    private AudioSource audioSource;

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnTriggerEnter(Collider other)
    {
        // Verificamos si lo que entró es la bola
        if (other.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();

            if (ballRigidbody != null)
            {
                // Aplicamos una fuerza instantánea hacia arriba (en el eje Y local del trampolín)
                ballRigidbody.AddForce(transform.up * jumpForce, ForceMode.Impulse);

                // Aplicamos amortiguación para un efecto más natural (opcional)
                ballRigidbody.linearDamping = dragOnJump;

                // Reproducimos el sonido si está configurado
                if (jumpSound != null && audioSource != null)
                {
                    audioSource.PlayOneShot(jumpSound);
                }
            }
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Restauramos el drag original de la bola al salir del trampolín (opcional)
        if (other.CompareTag("Player"))
        {
            Rigidbody ballRigidbody = other.GetComponent<Rigidbody>();
            if (ballRigidbody != null)
            {
                // Aquí deberías restaurar el drag original de la bola si lo cambiaste en el script del jugador.
                // Si no lo usas, puedes quitar este método.
                // Por ejemplo: ballRigidbody.drag = 0f;
            }
        }
    }
}