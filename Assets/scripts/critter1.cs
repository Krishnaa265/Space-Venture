using UnityEngine;

public class Critter1 : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Sprite[] sprites;

    private float moveSpeed;
    private Vector3 targetPosition;
    private Quaternion targetRotation;

    private float moveTimer;
   private float moveInterval;
    private ObjectPooler zappedeffectpool;
    private ObjectPooler burnEffectpool;

    void Start()
    {
        

        spriteRenderer = GetComponent<SpriteRenderer>();
        zappedeffectpool = GameObject.Find("critter1zappedpool").GetComponent<ObjectPooler>();
        burnEffectpool = GameObject.Find("critter1burntpool").GetComponent<ObjectPooler>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        moveSpeed = Random.Range(0.5f, 3f);
        GenerateRandomPosition();
       moveInterval = Random.Range(0.1f, 2f);
        moveTimer = moveInterval;
       

    }

    void Update()
    {
        targetPosition -= new Vector3(GameManager.Instance.worldSpeed * Time.deltaTime, 0);
        transform.position = Vector3.MoveTowards(transform.position, targetPosition, moveSpeed * Time.deltaTime);
        
        if (moveTimer > 0)
        {
            moveTimer -= Time.deltaTime;
        }
        else
        {
            GenerateRandomPosition();
            moveInterval = Random.Range(0.3f, 2f);
            moveTimer = moveInterval;
        }

        Vector3 relativePos = targetPosition - transform.position;
        if (relativePos != Vector3.zero)
        {
            targetRotation = Quaternion.LookRotation(Vector3.forward, relativePos);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, targetRotation, 1080 * Time.deltaTime);
        }
        float movex=GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-movex,0);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }
        /*
        targetPosition -= new Vector3(GameManager.Instance.worldSpeed * Time.deltaTime, 0);
      

     

    
    }

   
        
    */
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("bullet"))
        {
            GameObject zappedeffect = zappedeffectpool.GetPooledObject();
            zappedeffect.transform.position = transform.position;
            zappedeffect.transform.rotation = transform.rotation;
            zappedeffect.SetActive(true);
            // Instantiate(zappedEffect, transform.position,transform.rotation);
            gameObject.SetActive(false);
            audiomanager.Instance.PlayModifiedSound(audiomanager.Instance.squished);
            GameManager.Instance.crittercount++;
        }
        else if (collision.gameObject.CompareTag("Player"))
        {
            GameObject burnteffect = burnEffectpool.GetPooledObject();
            burnteffect.transform.position = transform.position;
            burnteffect.transform.rotation = transform.rotation;
            burnteffect.SetActive(true);
            gameObject.SetActive(false);
            audiomanager.Instance.PlayModifiedSound(audiomanager.Instance.burn);
            GameManager.Instance.crittercount++;
        }
    }



    private void GenerateRandomPosition()
    {

        float randomX = Random.Range(-5f, 5f);
        float randomY = Random.Range(-5f, 5f);
        targetPosition = new Vector2(randomX, randomY);
    }
    }