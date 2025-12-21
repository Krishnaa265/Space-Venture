using UnityEngine;

public class phasorweopon : weopen
{
    public static phasorweopon Instance;

    [SerializeField] private ObjectPooler bulletPool;
 

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

    public void Shoot()
    {
        audiomanager.Instance.PlayModifiedSound(audiomanager.Instance.shoot);
        for (int i = 0; i < stats[weoponlevel].amount; i++)
        {
           float ypos=transform.position.y;
           GameObject bullet = bulletPool.GetPooledObject();
            if (stats[weoponlevel].amount > 1) {
                float spacing = stats[weoponlevel].range / (stats[weoponlevel].amount - 1);
                 ypos = transform.position.y - (stats[weoponlevel].range / 2) + i * spacing;
             
            }
            bullet.transform.position = new Vector2(transform.position.x, ypos);
            bullet.transform.localScale = new Vector2(stats[weoponlevel].size, stats[weoponlevel].size);
            bullet.SetActive(true);
        }
    }
    public void levelup()
    {
       if(weoponlevel< stats.Count - 1)
        {
            weoponlevel++;
          
        }
    }
}

