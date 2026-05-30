using UnityEngine;

public class LooseCollider : MonoBehaviour
{
    public GameObject canvas;

        private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            canvas.SetActive(true);
        }
    }
}
