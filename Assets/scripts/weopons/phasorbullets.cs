using UnityEngine;

public class phasorbullets : MonoBehaviour
{
    phasorweopon weopon;
    private void Start()
    {
       weopon=phasorweopon.Instance;
    }
    void Update()
    {
        transform.position += new Vector3((weopon.stats[weopon.weoponlevel].speed * Time.deltaTime), 0f);
        if (transform.position.x > 9)
        {
            gameObject.SetActive(false);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("obstacle"))
        {
            astroid asteroid = collision.gameObject.GetComponent<astroid>();
            if (asteroid) asteroid.takedamage(weopon.stats[weopon.weoponlevel].damage,true);
            gameObject.SetActive(false);
        }
        
        else if (collision.gameObject.CompareTag("critter"))
        {
            gameObject.SetActive(false);
        }
        else if (collision.gameObject.CompareTag("enemy"))
        {
            enemy enem = collision.gameObject.GetComponent<enemy>();
            if (enem) enem.TakeDamage(weopon.stats[weopon.weoponlevel].damage);
            gameObject.SetActive(false);
        }
    }
}
 