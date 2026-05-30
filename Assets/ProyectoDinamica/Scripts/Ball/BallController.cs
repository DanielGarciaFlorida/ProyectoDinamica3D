using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class BallController : MonoBehaviour
{
    // Referencia al Rigidbody de la bola
    private Rigidbody rb;

    [SerializeField] private float maxSpeed = 20f;

    // Comprueba si la bola sigue moviéndose
    public bool IsMoving => rb.linearVelocity.magnitude > 0.1f;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        // Suaviza el movimiento visual
        rb.interpolation = RigidbodyInterpolation.Interpolate;

        // Mejora detección de colisiones rápidas
        rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
    }

    private void FixedUpdate()
    {
        LimitSpeed();
    }

    /// Limita la velocidad de la bola para evitar movimientos exagerados o bugs 
    private void LimitSpeed()
    {
        if (rb.linearVelocity.magnitude > maxSpeed)
        {
            rb.linearVelocity =
                rb.linearVelocity.normalized * maxSpeed;
        }
    }

    public void StopBall()
    {
        rb.linearVelocity = Vector3.zero;
        rb.angularVelocity = Vector3.zero;
    }

    /// Devuelve el Rigidbody de la bola
    public Rigidbody GetRigidBody()
    {
        return rb;
    }
}