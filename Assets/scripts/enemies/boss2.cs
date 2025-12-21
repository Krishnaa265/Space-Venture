using UnityEngine;

public class boss2 : enemy
{
    private Animator animator;
    private bool charging = true;

    public override void Awake()
    {
        base.Awake();
        animator = GetComponent<Animator>();
    }

    public override void OnEnable()
    {
        base.OnEnable();
        EnterIdleState();
    }
    public override void Start()
    {
        base.Start();
        destroyeffectpool = GameObject.Find("boom3pool").GetComponent<ObjectPooler>();
        hitsound = audiomanager.Instance.hitarmor;
        destroysound = audiomanager.Instance.boom2;

    }
    public override void Update()
    {
        base.Update();
        float position= PlayerController.Instance.transform.position.x;
        if(transform.position.y > 4 || transform.position.y < -4)
        {
            speedY *= -1;
        }
        if (transform.position.x > 7.5)
        {
            EnterIdleState();
        }
        else if (transform.position.x < -4 || transform.position.x < position)
        {
            EnterChargeState();
        }
        }
    private void EnterIdleState()
    {
        if (charging)
        {
            speedX = 0.2f;
            speedY = Random.Range(-1.2f, 1.2f);
            charging = false;
            animator.SetBool("charging",false);
        }
    }

    private void EnterChargeState()
    {
        if (!charging)
        {
            speedX = Random.Range(3.5f, 4);
            speedY = 0;
            charging = true;
            animator.SetBool("charging", true);
        }
    }

}
