using UnityEngine;

public class ChestBehavior : MonoBehaviour
{
    public static bool collected;

    private void OnCollisionEnter(Collision collision)
    {
        if (!collected)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                collected = true;
                Destroy(gameObject);
            }
        }
    }
}