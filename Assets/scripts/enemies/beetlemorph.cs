using UnityEngine;

public class Beetlemorph : enemy
{
    [SerializeField] private Sprite[] sprites;
    private float timer;
    private float frequency;
    private float amplitude;
    private float centery;
    public override void OnEnable()
    {
        base.OnEnable();
        timer = transform.position.y;
        frequency = Random.Range(0.3f,1f);
        amplitude = Random.Range(0.8f, 1.5f);
        centery=transform.position.y;
    }
    public override void Start()
    {
        base.Start();

        // Assign a random sprite
        if (sprites != null && sprites.Length > 0)
        {
            spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        }
        else
        {
            Debug.LogWarning("Beetlemorph: Sprites array is empty or not assigned.");
        }

        // Get the object pool
        GameObject poolObj = GameObject.Find("beetle_pop_pool");
        if (poolObj != null)
        {
            destroyeffectpool = poolObj.GetComponent<ObjectPooler>();
        }
        else
        {
            Debug.LogError("Beetlemorph: boom1pool GameObject not found!");
        }
        hitsound = audiomanager.Instance.beetlehit;
        destroysound = audiomanager.Instance.beetledestroy;
        speedX = Random.Range(-0.8f,-1.5f);
    }
    public override void Update()
    {
        base.Update();
        timer -= Time.deltaTime;
        float sine = Mathf.Sin(timer*frequency)*amplitude;
        transform.position = new Vector3(transform.position.x,centery+sine);
    }
}
