using UnityEngine;

public class QuestScript : MonoBehaviour
{
    public GameObject go;
    bool entered = false;

    private void OnTriggerEnter(Collider other)
    {
        if (!entered)
        {
            if (other.CompareTag("Player"))
            {
                go.SetActive(true);
            }
            entered = true;
        }
    }
}
