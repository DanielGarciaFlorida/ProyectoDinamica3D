using UnityEngine;
using UnityEngine.InputSystem;

public class BallShooter : MonoBehaviour
{
    // Referencias
    [SerializeField] private BallController ball;
    [SerializeField] private Transform pivotY;
    [SerializeField] private Transform pivotX;
    [SerializeField] private Transform hitPoint;

    [SerializeField] private float maxForce = 20f;
    [SerializeField] private float chargeSpeed = 10f;
    [SerializeField] private float maxClubAngle = 45f;

    private Rigidbody rb;
    private float currentForce;
    private bool charging;

    private Camera cam;

    private PlayerInputActions controls;
    private void Awake()
    {
        if (ball == null)
        {
            Debug.LogError("Ball NO asignada");
        }

        rb = ball.GetComponent<Rigidbody>();

        cam = Camera.main;

        controls = new PlayerInputActions();
    }

    private void OnEnable()
    {
        controls.Enable();

        // Evento al pulsar botón
        controls.Gameplay.Shoot.started += StartCharging;

        // Evento al soltar botón
        controls.Gameplay.Shoot.canceled += ReleaseShot;
    }

    private void OnDisable()
    {
        controls.Disable();

        controls.Gameplay.Shoot.started -= StartCharging;

        controls.Gameplay.Shoot.canceled -= ReleaseShot;
    }

    private void Update()
    {
        // No permitir disparar mientras la bola se mueve
        if (ball.IsMoving) return;

        // Actualiza dirección del palo
        AimClub();

        // Carga fuerza mientras mantenemos pulsado
        if (charging)
        {
            currentForce += chargeSpeed * Time.deltaTime;
            currentForce = Mathf.Clamp(currentForce, 0, maxForce);
        }

        // Animación del palo
        AnimateClub();
    }

    //Empieza a cargar fuerza
    private void StartCharging(InputAction.CallbackContext context)
    {
        charging = true;
    }

    // Suelta el golpe
    private void ReleaseShot(InputAction.CallbackContext context)
    {
        Shoot();

        charging = false;
        currentForce = 0;
    }

    //Aplica impulso físico a la bola
    private void Shoot()
    {
        Vector3 direction = pivotY.forward;
        direction.y = 0;
        direction.Normalize();
        rb.AddForce(direction * currentForce, ForceMode.Impulse);
    }

    //Apunta el palo hacia la posición del ratón
    private void AimClub()
    {
        Vector2 mousePosition = Mouse.current.position.ReadValue();

        Ray ray = cam.ScreenPointToRay(mousePosition);

        Plane plane = new Plane(Vector3.up, ball.transform.position);

        if (plane.Raycast(ray, out float distance))
        {
            Vector3 point = ray.GetPoint(distance);
            Vector3 dir = point - ball.transform.position;
            dir.y = 0;

            pivotY.forward = dir.normalized;
        }
    }

    //Animación visual del palo
    private void AnimateClub()
    {
        if (!charging)
        {
            pivotX.localRotation = Quaternion.Lerp(pivotX.localRotation, Quaternion.identity, Time.deltaTime * 5f);
            return;
        }

        float angle = Mathf.Lerp(0, maxClubAngle, currentForce / maxForce);
        pivotX.localRotation = Quaternion.Euler(-angle, 0, 0);
    }
}