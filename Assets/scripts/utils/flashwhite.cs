using System.Collections;
using UnityEngine;

public class FlashWhite : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;

    private Material defaultMaterial;
    private Material whiteMaterial;
    private Coroutine flashRoutine;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultMaterial = spriteRenderer.material;
        whiteMaterial = Resources.Load<Material>("Materials/mWhite");
    }

    void OnEnable()
    {
        // Reset material when re-enabled from pool
        if (spriteRenderer != null && defaultMaterial != null)
        {
            spriteRenderer.material = defaultMaterial;
        }
    }

    public void Flash()
    {
        if (flashRoutine != null)
            StopCoroutine(flashRoutine);

        flashRoutine = StartCoroutine(FlashRoutine());
    }

    IEnumerator FlashRoutine()
    {
        spriteRenderer.material = whiteMaterial;
        yield return new WaitForSeconds(0.1f);
        spriteRenderer.material = defaultMaterial;
        flashRoutine = null;
    }
}
