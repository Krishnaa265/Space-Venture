using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public float worldSpeed;
    public float adjustedworldSpeed;
    public int crittercount;
  private ObjectPooler boss1pool;
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
    {
        boss1pool = GameObject.Find("boss1pool").GetComponent<ObjectPooler>();  
        crittercount = 0;
    }
    void Update()
    {  adjustedworldSpeed = worldSpeed*Time.deltaTime;
        if (Input.GetKeyDown(KeyCode.P) || Input.GetKeyDown(KeyCode.Escape) || Input.GetButtonDown("Fire3"))
        {
            pause();

        }
        if (crittercount > 15)
        {
            crittercount = 0;
           GameObject boss1=boss1pool.GetPooledObject();
            boss1.transform.position = new Vector2(15f, 0);
            boss1.transform.rotation = Quaternion.Euler(0,0,-90); // Face upright
            boss1.SetActive(true);

        }
    }
    public void pause()
    {

        if (UIcontroller.Instance.pausepanel.activeSelf == false)
        {
            audiomanager.Instance.PlaySound(audiomanager.Instance.pause);
            UIcontroller.Instance.pausepanel.SetActive(true);
            Time.timeScale = 0;
        }
        else
        {
            audiomanager.Instance.PlaySound(audiomanager.Instance.unpause);
            UIcontroller.Instance.pausepanel.SetActive(false);
            Time.timeScale = 1;
            PlayerController.Instance.exitboost();
        }
    }
    public void quitgame()
    {
        Application.Quit();
    }
    public void gotomenu()
    {
        SceneManager.LoadScene("mainmenu");
    }

    public void gameOver()
    {
        StartCoroutine(ShowGameOverScreen());
    }

    IEnumerator ShowGameOverScreen()
    {
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("GameOver");
    }
    public void SetWorldSpeed(float speed)
    {
        worldSpeed = speed;
    }
}
