using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Manager : MonoBehaviour
{
    public static Manager Instance;

    // Player Information
    public TMP_InputField nicknameHolder;
    public Button startButton;
    public string nickname;

    void Awake()
    {
        /*
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        //DontDestroyOnLoad(gameObject);*/

        // Ensure the TMP_InputField and Button references are reassigned when the scene changes
    }

    void Start()
    {

        startButton.onClick.AddListener(StartSimulation);
    
        // If the nickname is saved in PlayerPrefs, load it
        if (PlayerPrefs.HasKey("Nickname"))
        {
            nicknameHolder.text = PlayerPrefs.GetString("Nickname");
        }
    }

    public void StartSimulation()
    {
        // Save the nickname to PlayerPrefs when starting the simulation
        nickname = nicknameHolder.text;
        PlayerPrefs.SetString("Nickname", nickname);  // Save nickname
        SceneManager.LoadScene(1);  // Load the next scene
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.ExitPlaymode();
#else
        Application.Quit(); // Quit the game when running outside of the editor
#endif
    }
}
