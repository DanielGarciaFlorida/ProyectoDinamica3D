using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(LineRenderer))]
public class LineToMouse3D : MonoBehaviour
{
    private LineRenderer lineRenderer;
    private Camera mainCamera;

    [Header("Referencias")]
    [SerializeField] private BallController scriptBola;

    [Header("Configuración de Colisión")]
    // Capas de los objetos que bloquearán la línea (por ejemplo: "Suelo", "Paredes", "Obstáculos")
    [SerializeField] private LayerMask capasObstaculos;

    void Start()
    {
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
        if (scriptBola != null && scriptBola.IsMoving)
        {
            lineRenderer.enabled = false;
            return;
        }

        lineRenderer.enabled = true;

        // 1. El origen siempre es la bola
        Vector3 origenBola = transform.position;
        lineRenderer.SetPosition(0, origenBola);

        // 2. Buscamos la posición del mouse proyectada en el plano de la bola
        Vector2 posicionMouse = Mouse.current.position.ReadValue();
        Ray rayoCamara = mainCamera.ScreenPointToRay(posicionMouse);
        Plane planoHorizontal = new Plane(Vector3.up, origenBola);

        float distanciaDelRayo;

        if (planoHorizontal.Raycast(rayoCamara, out distanciaDelRayo))
        {
            Vector3 puntoDestinoDeseado = rayoCamara.GetPoint(distanciaDelRayo);
            puntoDestinoDeseado.y = origenBola.y; // Mantener eje Y

            // 3. EL TRUCO DE LA PARED: Calculamos la dirección y distancia hacia el mouse
            Vector3 direccion = puntoDestinoDeseado - origenBola;
            float distanciaMax = direccion.magnitude;

            RaycastHit choqueObstaculo;

            // Lanzamos un rayo físico desde la bola hacia la posición del mouse
            if (Physics.Raycast(origenBola, direccion.normalized, out choqueObstaculo, distanciaMax, capasObstaculos))
            {
                // Si choca con una pared, el punto final será donde impactó el rayo
                lineRenderer.SetPosition(1, choqueObstaculo.point);
            }
            else
            {
                // Si el camino está limpio, la línea llega hasta el mouse de forma normal
                lineRenderer.SetPosition(1, puntoDestinoDeseado);
            }
        }
    }
}