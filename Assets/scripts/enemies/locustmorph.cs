using System.Collections.Generic;
using UnityEngine;
using static TMPro.SpriteAssetUtilities.TexturePacker_JsonArray;

public class locustmorph : enemy
{
    [SerializeField] private List<Frames> frames;
    private int enemyvarient;
    private bool charging;

    public override void OnEnable()
    {base.OnEnable();
        enemyvarient=Random.Range(0, frames.Count);
        EnterIdle();
    }
    public override void Start()
    {
        base.Start();
  
        GameObject poolObj = GameObject.Find("locustpoppool");
        if (poolObj != null)
        {
            destroyeffectpool = poolObj.GetComponent<ObjectPooler>();
        }
        hitsound = audiomanager.Instance.locusthit;
        destroysound = audiomanager.Instance.locustdestroy;
        speedX = Random.Range(0.1f, 0.6f);
        speedY = Random.Range(-0.9f, 0.9f);
    }
    public override void Update()
    {
        base.Update();
        if (transform.position.y > 4.5 || transform.position.y < -4.5)
        {
            speedY *= -1;
        }
    }
    private void EnterIdle()
    {
        charging = false;
       
            spriteRenderer.sprite = frames[enemyvarient].sprites[0];
            speedX = Random.Range(0.1f, 0.6f);
            speedY = Random.Range(-0.9f, 0.9f);
         
    }

    private void EnterCharge()
{
    if (!charging)
    {
        charging = true;
        spriteRenderer.sprite = frames[enemyvarient].sprites[1];
        audiomanager.Instance.PlaySound(audiomanager.Instance.locustcharge);
        speedX = Random.Range(-4f, -6f);
        speedY = 0;
    }
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (lives <= maxLives * 0.5f)
        {
            EnterCharge();
        }
    }
    [System.Serializable]
    private class Frames
    {
        public Sprite[] sprites;
    }
}
