
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    void Start()
    {
        Time.timeScale = 1f;
    }

    public void NewGame()
    {
        SceneManager.LoadScene("SampleScene");
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void Instructions()
    {
        SceneManager.LoadScene("instructions");
    }
    public void controls()
    {
        SceneManager.LoadScene("controls");
    }
    public void menu()
    {
        SceneManager.LoadScene("mainmenu");
    }

}

