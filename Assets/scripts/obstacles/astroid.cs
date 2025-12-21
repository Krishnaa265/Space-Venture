//using UnityEditor;
using UnityEngine;
using System.Collections;

public class astroid : MonoBehaviour
{
    private SpriteRenderer spriteRenderer;
   
    [SerializeField] private Material defaultmaterial;
    [SerializeField] private Material whitematerial;
    [SerializeField] private GameObject destroyEffect;

     private int lives;
    private int damage;
    private int experiencetogive = 1;

    float pushX ;
    float pushY ;

    private Rigidbody2D rb;
    [SerializeField] private Sprite[] sprites;
    void OnEnable()
    {
        lives = 5;
        transform.rotation = Quaternion.identity;
        pushX = Random.Range(-1f, 0);
        pushY = Random.Range(-1f, 1f);
      if(rb)rb.linearVelocity = new Vector2(pushX, pushY);

    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultmaterial =spriteRenderer.material;
       
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer.sprite = sprites[Random.Range(0, sprites.Length)];
        pushX = Random.Range(-1f, 0);
        pushY = Random.Range(-1f, 1f);
        if (rb) rb.linearVelocity = new Vector2(pushX, pushY);

        float randomScale = Random.Range(0.6f, 1f);
        transform.localScale = new Vector2(randomScale, randomScale);

        lives = 5;
        damage = 10;


    }
   

    // Update is called once per frame
    void Update()
    {float moveX= GameManager.Instance.worldSpeed * Time.deltaTime;
        transform.position += new Vector3(-moveX, 0);
        if(transform.position.x < -11)
        {
            gameObject.SetActive(false);
        }
    }
    private void OnCollisionEnter2D(Collision2D collision)

    {
        if (collision.gameObject.CompareTag("Player"))
        {
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (player) player.TakeDamage(damage);
        }


    }
    public void takedamage(int damage,bool givexperience)
    {

        if (lives > 0)
        {
            spriteRenderer.material = whitematerial;
            StartCoroutine("resetmaterial");
        }
        lives-=damage;//
        audiomanager.Instance.PlayModifiedSound(audiomanager.Instance.hitrock);//

       if(lives <= 0)
        {
            Instantiate(destroyEffect, transform.position, transform.rotation);
            audiomanager.Instance.PlayModifiedSound(audiomanager.Instance.boom2);
            spriteRenderer.material = defaultmaterial;
            gameObject.SetActive(false);
          if(givexperience)  PlayerController.Instance.getexperience(experiencetogive);
        }
    }
    IEnumerator resetmaterial()

    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultmaterial;
    }
   
}
