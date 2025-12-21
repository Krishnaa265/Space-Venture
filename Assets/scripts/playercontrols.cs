using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem.XR;
using UnityEngine.SceneManagement;
using System.Collections;
using NUnit.Framework;
using System.Collections.Generic;

public class PlayerController : MonoBehaviour
{
    public static PlayerController Instance;
    private Rigidbody2D rb;
    private Animator animator;
    private Vector2 playerDirection;

    [SerializeField] private float energy;
    [SerializeField] private float maxEnergy;
    [SerializeField] private float energyRegen;
    private SpriteRenderer spriteRenderer;
    [SerializeField] private Material defaultmaterial;
    [SerializeField] private Material whitematerial;


    [SerializeField] private float health;
    [SerializeField] private float maxhealth;
     private ObjectPooler destroyeffectpool;
    [SerializeField] private ParticleSystem engineEffect;

    [SerializeField] private float moveSpeed;
  //  public float boost = 1f;
  //  private float boostpower = 5f;
    public bool boosting = false;

    [SerializeField] private int experience;
    [SerializeField] private int currentlevel;
    [SerializeField] private int maxlevel;
    [SerializeField] private List <int> playerlevels ;

    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
        }
        else
        {
            Instance = this;
        }
    }
    void Start()
    {  for(int i = playerlevels.Count; i < maxlevel; i++)
        {
            playerlevels.Add(Mathf.CeilToInt(playerlevels[playerlevels.Count-1]*1.1f));
        }

        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        defaultmaterial=spriteRenderer.material;
        energy = maxEnergy;
        UIcontroller.Instance.UpdateEnergySlider(energy, maxEnergy);
        health = maxhealth;
        UIcontroller.Instance.UpdateHealthSlider(health, maxhealth);
        destroyeffectpool = GameObject.Find("boom1pool").GetComponent<ObjectPooler>();
        experience = 0;
        UIcontroller.Instance.UpdateExperienceSlider(experience, playerlevels[currentlevel]);

    }

    void Update()
    {
        if (Time.timeScale > 0)
        {
            float directionX = Input.GetAxisRaw("Horizontal");
            float directionY = Input.GetAxisRaw("Vertical");

            // Update Animator parameters for 2D Blend Tree
            animator.SetFloat("movex", directionX);
            animator.SetFloat("movey", directionY);

            // Normalize to avoid faster diagonal movement
            playerDirection = new Vector2(directionX, directionY).normalized;
            if (Input.GetKeyDown(KeyCode.LeftShift) || Input.GetButtonDown("Fire2")) { enterboost(); }
            else if (Input.GetKeyUp(KeyCode.LeftShift) || Input.GetButtonUp("Fire2")) { exitboost(); }
            if (Input.GetKeyDown(KeyCode.RightShift) || Input.GetButtonDown("Fire1"))
            {
                phasorweopon.Instance.Shoot();
            }
        }
    }

    void FixedUpdate()
    {
        rb.linearVelocity = new Vector2(
            playerDirection.x * moveSpeed,
            playerDirection.y * moveSpeed
        );
        if (boosting)
        {
            if (energy >= 0.5f) energy -= 0.8f;
            else
            {
                exitboost();
            }
        }
        else
        {
            if (energy < maxEnergy)
            {
                energy += energyRegen;
            }
        }
        UIcontroller.Instance.UpdateEnergySlider(energy, maxEnergy);

    }
    void enterboost()
    { 
        if (energy > 10)
        {

            audiomanager.Instance.PlaySound(audiomanager.Instance.fire);
            engineEffect.Play();
            animator.SetBool("boosting", true);
            GameManager.Instance.SetWorldSpeed(7f);
            boosting = true;
        }

    }
    public void exitboost()
    {
        animator.SetBool("boosting", false);
        GameManager.Instance.SetWorldSpeed(1f);
        boosting = false;
    }
    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            astroid asteroid = collision.gameObject.GetComponent<astroid>();
            if (asteroid) asteroid.takedamage(3,false);
        }
        if (collision.gameObject.CompareTag("enemy"))
        {
            enemy enem = collision.gameObject.GetComponent<enemy>();
            if (enem) enem.TakeDamage(3);
        }
    }

    public void TakeDamage(int damage)
    {
        health -= damage;
        UIcontroller.Instance.UpdateHealthSlider(health, maxhealth);
        spriteRenderer.material = whitematerial;
        StartCoroutine("resetmaterial");
        audiomanager.Instance.PlaySound(audiomanager.Instance.hit);
        if (health <= 0)
        {
            exitboost();
            GameManager.Instance.SetWorldSpeed(0f);
            gameObject.SetActive(false);
            GameObject destroyeffect = destroyeffectpool.GetPooledObject();
            if (destroyeffect != null)
            {
                destroyeffect.transform.position = transform.position;
                destroyeffect.transform.rotation = transform.rotation;
                destroyeffect.SetActive(true);
            }
            else
            {
                Debug.LogWarning(" destroyeffect was null (possibly destroyed outside the pool)");
            }


            GameManager.Instance.gameOver();
            audiomanager.Instance.PlaySound(audiomanager.Instance.ice);

        }
    }
    IEnumerator resetmaterial()
    {
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.material = defaultmaterial;
    }
    public void getexperience(int exp) {
        experience += exp;
        UIcontroller.Instance.UpdateExperienceSlider(experience, playerlevels[currentlevel]);
        if (experience > playerlevels[currentlevel])
        {
            levelup();
        }
    }
    public void levelup()
    {
        experience -=playerlevels[currentlevel];
        if(currentlevel< maxlevel - 1){
            currentlevel++;
            UIcontroller.Instance.UpdateExperienceSlider(experience, playerlevels[currentlevel]);
            phasorweopon.Instance.levelup();
           // maxhealth+=2;
            health = maxhealth;
            UIcontroller.Instance.UpdateHealthSlider(health,maxhealth);
        }
    }
}

  
