using UnityEngine;

public class floatinspace : MonoBehaviour
{
    void Update()
    {
        float moveX = GameManager.Instance.adjustedworldSpeed ;
        transform.position += new Vector3(-moveX, 0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
    }
}
