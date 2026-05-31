using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class TrayectoryRenderer : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;

    [SerializeField] private BallController scriptBola;
    BallShooter shooter;

    
    // Capas de los objetos que bloquearán la línea
    [SerializeField] private LayerMask capasObstaculos;

    void Start()
    {
        shooter = FindAnyObjectByType<BallShooter>();

        lineRenderer = GetComponent<LineRenderer>();
        mainCamera = Camera.main;
        lineRenderer.positionCount = 2;
        lineRenderer.useWorldSpace = true;

        if (scriptBola == null)
        {
            scriptBola = GetComponent<BallController>();
            
        }
    }

    void Update()
    {
        //Si la bola está en movimiento o el shooter ha disparado, no mostramos la línea
        if ((scriptBola != null && scriptBola.IsMoving) || (shooter != null && shooter.HasShot))
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        //La bola como origen de la línea
        Vector3 origenBola = transform.position;
        lineRenderer.SetPosition(0, origenBola);

        //El ratón como final
        Vector2 posicionMouse = Mouse.current.position.ReadValue();
        Ray rayoCamara = mainCamera.ScreenPointToRay(posicionMouse);
        Plane planoHorizontal = new Plane(Vector3.up, origenBola);

        float distanciaDelRayo;

        if (planoHorizontal.Raycast(rayoCamara, out distanciaDelRayo))
        {
            Vector3 puntoDestinoDeseado = rayoCamara.GetPoint(distanciaDelRayo);
            //La línea no se mueve en el eje Y, siempre se mantiene a la altura de la bola
            puntoDestinoDeseado.y = origenBola.y;

            Vector3 direccion = puntoDestinoDeseado - origenBola;
            float distanciaMax = direccion.magnitude;

            RaycastHit choqueObstaculo;

            
            if (Physics.Raycast(origenBola, direccion.normalized, out choqueObstaculo, distanciaMax, capasObstaculos))
            {
                
                lineRenderer.SetPosition(1, choqueObstaculo.point);
            }
            else
            {
                
                lineRenderer.SetPosition(1, puntoDestinoDeseado);
            }
        }
    }
}