using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;


public class InfoPopup : MonoBehaviour
{
    public TMP_Text Name;
    public TMP_Text Data;
    
    void Start()
    {
        Hide(); // Ensure the popup is hidden initially
    }
    public void SetContent(string name, string data)
    {
        Name.text = name;
        Data.text = data;
    }

    // Show the popup
    public void Show()
    {
        gameObject.SetActive(true);
    }

    // Hide the popup
    public void Hide()
    {
        gameObject.SetActive(false);
    }
}
