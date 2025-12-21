using UnityEngine;

public class audiomanager : MonoBehaviour
{
    public static audiomanager Instance;
    public AudioSource ice;
    public AudioSource fire;
    public AudioSource hit;
    public AudioSource pause;
    public AudioSource unpause;
    public AudioSource boom2;
    public AudioSource hitrock;
    public AudioSource squished;
    public AudioSource shoot;
    public AudioSource burn;
    public AudioSource hitarmor;
    public AudioSource bosscharge;
    public AudioSource bossspawn;
    public AudioSource beetlehit;
    public AudioSource beetledestroy;
    public AudioSource locustdestroy;
    public AudioSource locusthit;
    public AudioSource locustcharge;

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

    public void PlaySound(AudioSource sound)
    {
        sound.Stop();
        sound.Play();
    }
    public void PlayModifiedSound(AudioSource sound)
    {
        sound.pitch = Random.Range(0.7f, 1.3f);
        sound.Stop();
        sound.Play();
    }
}
