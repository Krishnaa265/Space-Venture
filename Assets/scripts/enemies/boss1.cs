using UnityEngine;

public class boss1 : enemy
{
    //private ObjectPooler destroyeffectpool;
    private Animator animator;
    
    private bool charging;
    private float switchInterval;
    private float switchTimer;
  

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
        gameObject.SetActive(false);
    }
    public override void OnEnable()
    {
        base.OnEnable();
      //  lives = maxLives;
        EnterChargeState();
        audiomanager.Instance.PlaySound(audiomanager.Instance.bossspawn);
    }
    public override void Start()
    {
        base.Start();
        destroyeffectpool = GameObject.Find("boom3pool").GetComponent<ObjectPooler>();
        // audiomanager.Instance.PlaySound(audiomanager.Instance.bossspawn);
        //  lives = maxlives;
        hitsound = audiomanager.Instance.hitarmor;
        destroysound = audiomanager.Instance.boom2;
        //animator = GetComponent<Animator>();
       // EnterChargeState();
    }
    public override void Update()
    {
        base.Update();
        float playerPosition = PlayerController.Instance.transform.position.x;

        if (switchTimer > 0)
        {
            switchTimer -= Time.deltaTime;
        }
        else
        {
            if (charging && transform.position.x > playerPosition)
            {
                EnterPatrolState();
            }
            else
            {
                EnterChargeState();
            }
        }
        if (transform.position.y > 3 || transform.position.y < -3)
        {
            speedY *= -1;
        }
      else if (transform.position.x < playerPosition)
       {
         EnterChargeState();
       }
        bool boost = PlayerController.Instance.boosting;
        float moveX;
        if (boost && !charging)
        {
            moveX = GameManager.Instance.worldSpeed * Time.deltaTime * -0.5f;
        }
        else
        {
            moveX = speedX * Time.deltaTime;
        }
        float moveY = speedY * Time.deltaTime;

        transform.position += new Vector3(moveX, moveY);
        if (transform.position.x < -11)
        {
            Destroy(gameObject);
        }

    }
    void EnterPatrolState()
    {
        speedX = 0;
        speedY = Random.Range(-1f, 1f);
        switchInterval = Random.Range(5f, 10f);
        switchTimer = switchInterval;
        charging = false;
       animator.SetBool("charging", false);
    }

    void EnterChargeState()
    { 
        speedX = -5f;
        speedY = 0;
        switchInterval = Random.Range(0.6f, 1.3f);
        switchTimer = switchInterval;
        charging = true;
       animator.SetBool("charging", true);
        if (!charging) audiomanager.Instance.PlaySound(audiomanager.Instance.bosscharge);

    }
    public override void OnCollisionEnter2D(Collision2D collision)
    {
        base.OnCollisionEnter2D(collision);
        if (collision.gameObject.CompareTag("obstacle"))
        {
            astroid asteroid = collision.gameObject.GetComponent<astroid>();
            if (asteroid) asteroid.takedamage(damage,false);
        }
      
    }
  
}

/*
   

   

   

    void Start()
    {
       

       
        EnterChargeState();
      

    void Update()
    {
        float playerPosition = PlayerController.Instance.transform.position.x;

       

        if (transform.position.y > 3 || transform.position.y < -3){
            speedY *= -1;
        } else if (transform.position.x < playerPosition){
            EnterChargeState();
        }

       
    }



   
       
    }

  
}

*/