using UnityEngine;
using UnityEngine.SceneManagement;

/// <summary>
/// Su dung class nay de chuyen scene thong qua trigger event cua nhan vat voi cac object trong game
/// </summary>
public class ScenePortal : MonoBehaviour
{
    [Header("Khai bao scene hoac Object de turn on trigger")]
    [SerializeField] string? sceneName;
    [SerializeField] GameObject? Object;
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Debug.Log("collider sample " + collision.name);
            SceneManager.LoadScene(sceneName);
            // Object.SetActive(true);
        }
    }
}
