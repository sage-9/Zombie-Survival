using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.InputSystem;

public class MainMenu : MonoBehaviour
{
    public PlayerInput playerInput;

    void Start()
    {

    }
    public void ExitButton() {
        Application.Quit();
        Debug.Log("Game closed");

    }

    public void StartGame() {
        SceneManager.LoadScene("MainScene");
    }
}
