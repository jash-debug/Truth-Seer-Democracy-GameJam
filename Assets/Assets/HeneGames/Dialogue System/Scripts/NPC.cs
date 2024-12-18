using UnityEngine;
using DialogueEditor;

public class NPC : MonoBehaviour
{
    // NPC Conversation variable (assigned in the Inspector)
    public NPCConversation Conversation;

    // Method triggered when a collider enters the trigger zone
    private void OnTriggerEnter(Collider other)
    {
        // Check if the entering collider is the player (you can tag the player as "Player")
        if (other.CompareTag("Player"))
        {
            // Start the conversation
            ConversationManager.Instance.StartConversation(Conversation);
        }
    }
}
