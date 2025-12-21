using UnityEngine;
using UnityEngine.SceneManagement;

public class lostwhale : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collider)
    {
        if (collider.gameObject.CompareTag("Player"))
        {
            SceneManager.LoadScene("Level1 complete");
        }
    }
}