//using TreeEditor;
using UnityEngine;
//using static UnityEngine.RuleTile.TilingRuleOutput;

public class enemy : MonoBehaviour
{
    private FlashWhite flashWhite;

    protected ObjectPooler destroyeffectpool;
   
    [SerializeField] protected int lives;
    [SerializeField] protected int maxLives;
    [SerializeField] protected int damage;
    [SerializeField] protected int experienceToGive;

    protected SpriteRenderer spriteRenderer;
    protected AudioSource hitsound;
    protected AudioSource destroysound;
    protected float speedX = 0;
    protected float speedY = 0;

    public virtual void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        flashWhite = GetComponent<FlashWhite>();

    }
    public virtual void OnEnable()
    {
        lives = maxLives;
        if (flashWhite != null)
        {
            var sr = GetComponent<SpriteRenderer>();
            if (sr != null)
                sr.material = sr.sharedMaterial;
        }
    }

    public virtual void Start()
    {
      //  flashWhite = GetComponent<FlashWhite>();


    }
    public virtual void Update()
    {
        transform.position += new Vector3(speedX* Time.deltaTime,speedY*Time.deltaTime);
    }

    public virtual void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player)
            {
                player.TakeDamage(damage);
            }
        }
    }
    public virtual void TakeDamage(int damage)
    {
        lives -= damage;
        audiomanager.Instance.PlayModifiedSound(hitsound);
        if (lives > 0)
        {
            // Flash white if hit but not dead
            if (flashWhite != null)
                flashWhite.Flash();
        }
        else
        {
            audiomanager.Instance.PlayModifiedSound(destroysound);
            // Reset flash if dying
          //  if (flashWhite != null)
              //  flashWhite.Reset();

            // Spawn destruction effect
            if (destroyeffectpool != null)
            {
                GameObject destroyEffect = destroyeffectpool.GetPooledObject();

                if (destroyEffect != null)
                {
                    destroyEffect.transform.position = transform.position;
                    destroyEffect.transform.rotation = transform.rotation;
                    destroyEffect.SetActive(true); // Important to trigger animation
                }
                else
                {
                    Debug.LogWarning("Destroy effect object is null from pool.");
                }
            }
            else
            {
                Debug.LogWarning("Destroy effect pool not assigned.");
            }
            PlayerController.Instance.getexperience(experienceToGive);
            // Deactivate enemy
            gameObject.SetActive(false);
        }
    }

}
