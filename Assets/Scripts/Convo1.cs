using UnityEngine;
using DialogueEditor;
 
public class Convo1 : MonoBehaviour, IInteractable
{
    [SerializeField] private NPCConversation myConversation;
    public GameObject player;
    public float originalvalue;
    Movement mouseLookScript;
    public void Interact()
    {
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        mouseLookScript = player.GetComponent<Movement>();
        originalvalue = mouseLookScript.mouseSensitivity;
        mouseLookScript.mouseSensitivity = 0f;

        ConversationManager.OnConversationEnded += LockCursorBack;
        ConversationManager.Instance.StartConversation(myConversation);
    }

    private void LockCursorBack()
    {
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        ConversationManager.OnConversationEnded -= LockCursorBack;
        mouseLookScript.mouseSensitivity = originalvalue;
    }

}
