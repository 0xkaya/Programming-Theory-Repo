using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIMainScene : MonoBehaviour
{
    public static UIMainScene Instance { get; private set; }
    
    public interface IUIInfoContent
    {
        string GetName();
        string GetData();
    }
    
    public InfoPopup InfoPopup;
    protected IUIInfoContent m_CurrentContent;

    private void Awake(){
        Instance = this;
    }
    private void OnDestroy()
    {
        Instance = null;
    }
    public void SetNewInfoContent(IUIInfoContent content)
    {
        if (content == null)
        {
            Debug.LogWarning("Content is null! Cannot update InfoPopup.");
            InfoPopup.Hide();  
            return;
        }

        m_CurrentContent = content;
        InfoPopup.SetContent(content.GetName(), content.GetData()); // Update the popup
        InfoPopup.Show(); // Show the popup
    }

    public void HideInfoPopup()
    {
        InfoPopup.Hide(); // Hide the popup
    }
}