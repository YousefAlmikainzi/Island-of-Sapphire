using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Pool;

public class CollectedItem : MonoBehaviour
{
    bool bringIt;
    public GameObject go;
    public Vector3 questPos = Vector3.zero;

    private void OnTriggerEnter(Collider other)
    {
        if (!bringIt && ChestBehavior.collected)
        {
            if (other.CompareTag("quest"))
            {
                Instantiate(go, questPos, Quaternion.identity);
            }
            bringIt = true;
        }
    }
}