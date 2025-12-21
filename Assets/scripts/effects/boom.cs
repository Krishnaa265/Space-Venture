using UnityEngine;
using System.Collections;

public class boom : MonoBehaviour
{
    Animator animator;

    void Start()
    {
        animator = GetComponent<Animator>();
        StartCoroutine(DestroyAfterAnimation());
    }

    IEnumerator DestroyAfterAnimation()
    {
        yield return null; // Wait a frame for animator state to load
        float delay = animator.GetCurrentAnimatorStateInfo(0).length;
        yield return new WaitForSeconds(delay);
        Destroy(gameObject);
    }

    void Update()
    {
        float movex = GameManager.Instance.worldSpeed  * Time.deltaTime;
        transform.position += new Vector3(-movex, 0, 0);  // ✅ Keeps original y & z
    }
}
