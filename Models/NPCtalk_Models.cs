using UnityEngine;

public class NPCtalk_Models : MonoBehaviour
{
    public int npcID;
    public string npcName;
    public string? npcSpeak;


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log($"Player entered NPC {npcName} area.");
            // You can add code here to trigger dialogue or other interactions
        }
    }

}
