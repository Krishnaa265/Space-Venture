using UnityEngine;

public class whales : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created


    // Update is called once per frame
    void Update()
    {
        float moveX = GameManager.Instance.worldSpeed  * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
}
