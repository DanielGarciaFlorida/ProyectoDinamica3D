using UnityEngine;

public class GoalHole : MonoBehaviour
{
    public GameObject Canvas;
    // Update is called once per frame
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Canvas.SetActive(true);
        }
    }
}
