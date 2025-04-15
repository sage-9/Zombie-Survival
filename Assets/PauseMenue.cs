
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenu : MonoBehaviour
{
    [SerializeField] private GameObject pauseMenu;
    private bool GameIsPaused = false;

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (GameIsPaused)
            {
                Resume();
            }
            else
            {
                Pause();
            }
        }
    }

    public void Pause()
{
    pauseMenu.SetActive(true);            
    Time.timeScale = 0f;                  
    GameIsPaused = true;                   
    Cursor.lockState = CursorLockMode.None; 
    Cursor.visible = true;                 
}

    public void Resume()
   {
    pauseMenu.SetActive(false);            
    Time.timeScale = 1f;                   
    GameIsPaused = false;                  
    Cursor.lockState = CursorLockMode.Locked;
    Cursor.visible = false;                
   }

    public void LoadMenu()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainMenu");
    }

    public void QuitGame()
    {
        Debug.Log("Quitting game...");
        Application.Quit();
    }
}
