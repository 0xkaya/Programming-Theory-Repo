using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class SimulationManager : MonoBehaviour
{
    
    public TMP_Text UserName; // Display highest score with nickname


    void Start()
    {
         string nickname = PlayerPrefs.GetString("Nickname");
        // Display the highest score with the player's nickname
        if (Manager.Instance != null)
        {
            UserName.text = "Simulator | | \n"+ Manager.Instance.nickname;   
        }
        UserName.text = "Simulator | | \n"+ nickname;   

    }
}
